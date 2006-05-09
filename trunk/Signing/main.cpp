#include <stdio.h>
#include <windows.h>
#include <wincrypt.h>
#define MY_TYPE  (PKCS_7_ASN_ENCODING | X509_ASN_ENCODING)
#define MAX_NAME  128

//--------------------------------------------------------------------
//   SIGNER_NAME is used with the CertFindCertificateInStore function 
//   to retrieve the certificate of the message signer.
//   Replace the Unicode string below with the certificate subject 
//   name of the message signer.

#define SIGNER_NAME L"Carsten Sørensen"

//--------------------------------------------------------------------
//    The local function HandleError is declared here and
//    defined after main.

void HandleError( char *s);

//--------------------------------------------------------------------
//  The local function ShowBytes is declared here and defined after 
//  main.

void ShowBytes(BYTE *s, DWORD len);

//--------------------------------------------------------------------
//   Declare local functions SignAndEncrypt, DecryptAndVerify, and 
//   WriteSignedAndEncryptedBlob.
//   These functions are defined after main.

BYTE *SignAndEncrypt(
					 const BYTE     *pbToBeSignedAndEncrypted,
					 DWORD          cbToBeSignedAndEncrypted,
					 DWORD          *pcbSignedAndEncryptedBlob);

BYTE *DecryptAndVerify(
					   BYTE  *pbSignedAndEncryptedBlob,
					   DWORD  cbSignedAndEncryptedBlob);

void WriteSignedAndEncryptedBlob(
								 DWORD  cbBlob,
								 BYTE   *pbBlob);

void main (void)
{

	//--------------------------------------------------------------------
	//  Declare and initialize local variables.

	//--------------------------------------------------------------------
	//   pbToBeSignedAndEncrypted is the message to be 
	//   encrypted and signed.

	const BYTE     *pbToBeSignedAndEncrypted =
		(const unsigned char *)"Insert the message to be signed here";
	//--------------------------------------------------------------------
	//    This is the length of the message to be
	//    encrypted and signed. Note that it is one
	//    more that the length returned by strlen()
	//    to include the terminal NULL.

	DWORD          cbToBeSignedAndEncrypted  = 
		strlen((const char *)pbToBeSignedAndEncrypted)+1;

	//--------------------------------------------------------------------
	//    Pointer to a buffer that will hold the
	//    encrypted and signed message.

	BYTE                 *pbSignedAndEncryptedBlob;

	//--------------------------------------------------------------------
	//    A double word to hold the length of the signed 
	//    and encrypted message.

	DWORD                 cbSignedAndEncryptedBlob;
	BYTE                  *pReturnMessage;

	//------------------------------------------------------
	//  Call the local function SignAndEncrypt.
	//  This function returns a pointer to the 
	//  signed and encrypted BLOB and also returns
	//  the length of that BLOB.

	pbSignedAndEncryptedBlob = SignAndEncrypt(
		pbToBeSignedAndEncrypted,
		cbToBeSignedAndEncrypted,
		&cbSignedAndEncryptedBlob);

	printf("The following is the signed and encrypted message.\n");
	ShowBytes(pbSignedAndEncryptedBlob,cbSignedAndEncryptedBlob/4);

	// Open a file and write the signed and encrypted message to the file.

	WriteSignedAndEncryptedBlob(
		cbSignedAndEncryptedBlob,
		pbSignedAndEncryptedBlob);

	//--------------------------------------------------------------
	//   Call the local function DecryptAndVerify.
	//   This function decrypts and displays the 
	//   encrypted message and also verifies the 
	//   message's signature.

	if(pReturnMessage = DecryptAndVerify(
		pbSignedAndEncryptedBlob,
		cbSignedAndEncryptedBlob))
	{
		printf("    The returned, verified message is ->\n%s\n",pReturnMessage);
		printf("    The program executed without error.\n");
	}
	else
	{
		printf("Verification failed.\n");
	}

} // End Main.

//--------------------------------------------------------------------
//     Begin definition of the SignAndEncrypt function.

BYTE *SignAndEncrypt(
					 const BYTE     *pbToBeSignedAndEncrypted,
					 DWORD          cbToBeSignedAndEncrypted,
					 DWORD          *pcbSignedAndEncryptedBlob)
{

	//--------------------------------------------------------------------
	//   Declare and initialize local variables.

	FILE  *hToSave;
	HCERTSTORE              hCertStore;

	//--------------------------------------------------------------------
	//   pSignerCertContext will be the certificate of 
	//   the message signer.

	PCCERT_CONTEXT          pSignerCertContext ;

	//--------------------------------------------------------------------
	//   pReceiverCertContext will be the certificate of the 
	//   message receiver.

	PCCERT_CONTEXT          pReceiverCertContext;

	char pszNameString[256];
	CRYPT_SIGN_MESSAGE_PARA       SignPara;
	CRYPT_ENCRYPT_MESSAGE_PARA    EncryptPara;
	DWORD                         cRecipientCert;
	PCCERT_CONTEXT                rgpRecipientCert[5];
	BYTE                          *pbSignedAndEncryptedBlob = NULL;
	CERT_NAME_BLOB                Subject_Blob;
	BYTE                          *pbDataIn;
	DWORD                         dwKeySpec;
	HCRYPTPROV                    hCryptProv;

	//--------------------------------------------------------------------
	//     Open the MY certificate store. 
	//     For details, see the CertOpenStore function 
	//     PSDK reference page. 
	//     Note: case is not significant in certificate store names.

	if ( !( hCertStore = CertOpenStore(
		CERT_STORE_PROV_SYSTEM,
		0,
		NULL,
		CERT_SYSTEM_STORE_CURRENT_USER,
		L"my")))
	{
		HandleError("The MY store could not be opened.");
	}

	//--------------------------------------------------------------------
	// Get the certificate for the signer.

	if(!(pSignerCertContext = CertFindCertificateInStore(
		hCertStore,
		MY_TYPE,
		0,
		CERT_FIND_SUBJECT_STR,
		SIGNER_NAME,
		NULL)))
	{
		HandleError("Cert not found.\n");
	}

	//--------------------------------------------------------------------
	//   Get and print the name of the message signer.
	//   The following two calls to CertGetNameString with different
	//   values for the second parameter get two different forms of the
	//   certificate subject's name.

	if(CertGetNameString(
		pSignerCertContext ,
		CERT_NAME_SIMPLE_DISPLAY_TYPE,
		0,
		NULL,
		pszNameString,
		MAX_NAME) > 1)
	{
		printf("The SIMPLE_DISPLAY_TYPE message signer's name is  %s \n",pszNameString);
	}
	else
	{
		HandleError("Getting the name of the signer failed.\n");
	}

	if(CertGetNameString(
		pSignerCertContext,
		CERT_NAME_RDN_TYPE,
		0,
		NULL,
		pszNameString,
		MAX_NAME) > 1)
	{
		printf("The RDM_TYPE message signer's name is  %s \n",pszNameString);
	}
	else
	{
		HandleError("Getting the name of the signer failed.\n");
	}

	if(!( CryptAcquireCertificatePrivateKey(
		pSignerCertContext,
		0,
		NULL,
		&hCryptProv,
		&dwKeySpec,
		NULL)))
	{
		HandleError("CryptAcquireCertificatePrivateKey.\n");
	}

	//--------------------------------------------------------------------
	// Get the certificate for the receiver. In this case, a BLOB with the 
	// name of the receiver is saved in a file.

	// Note: To decrypt the message signed and encrypted here,
	// this program must use the certificate of the intended receiver. 
	// The signed and encrypted message can only be decrypted and verified
	// by the owner of the recipient certificate. That user must have
	// access to the private key associated with the public key of the
	// recipient's certificate.

	// To run this sample, the file contains information that allows the
	// program to find one of the current user's certificates. The 
	// current user should have access to the private key of the
	// certificate and thus can test the verification and decryption. 

	// In normal use, the file would contain information used to find
	// the certificate of an intended receiver of the message. 
	// The signed and encrypted message would be written
	// to a file or otherwise sent to the intended receiver.

	//--------------------------------------------------------------------
	//  Open a file and read in the receiver name
	//  BLOB.


	if( !(hToSave= fopen("s.txt","rb")))
	{
		//HandleError("Source file was not opened.\n");
	}

	fread(
		&(Subject_Blob.cbData),
		sizeof(DWORD),
		1,
		hToSave);

	if(ferror(hToSave))
	{
		HandleError("The size of the BLOB was not read.\n");
	}

	if(!(pbDataIn = (BYTE *) malloc(Subject_Blob.cbData)))
	{
		HandleError("Memory allocation error.");
	}

	fread(
		pbDataIn,
		Subject_Blob.cbData,
		1,
		hToSave);

	if(ferror(hToSave))
	{
		HandleError("BLOB not read.");
	}

	fclose(hToSave);

	Subject_Blob.pbData = pbDataIn;

	//--------------------------------------------------------------------
	//  Use the BLOB just read in from the file to find its associated
	//  certificate in the MY store.
	//  This call to CertFindCertificateInStore uses the
	//  CERT_FIND_SUBJECT_NAME dwFindType.

	if(!(pReceiverCertContext = CertFindCertificateInStore(
		hCertStore,
		MY_TYPE,
		0,
		//CERT_FIND_SUBJECT_NAME,
		//&Subject_Blob,
		CERT_FIND_SUBJECT_STR,
		SIGNER_NAME,
		NULL)))
	{
		HandleError("Receiver certificate not found.");
	}

	//--------------------------------------------------------------------
	//  Get and print the subject name from the receiver's certificate.

	if(CertGetNameString(
		pReceiverCertContext ,
		CERT_NAME_SIMPLE_DISPLAY_TYPE,
		0,
		NULL,
		pszNameString,
		MAX_NAME) > 1)
	{
		printf("The message receiver is  %s \n",pszNameString);
	}
	else
	{
		HandleError("Getting the name of the receiver failed.\n");
	}

	//--------------------------------------------------------------------
	//  Initialize variables and data structures
	//  for the call to CryptSignAndEncryptMessage.

	SignPara.cbSize = sizeof(CRYPT_SIGN_MESSAGE_PARA);
	SignPara.dwMsgEncodingType = MY_TYPE;
	SignPara.pSigningCert = pSignerCertContext ;
	SignPara.HashAlgorithm.pszObjId = szOID_RSA_MD2;
	SignPara.HashAlgorithm.Parameters.cbData = 0;
	SignPara.pvHashAuxInfo = NULL;
	SignPara.cMsgCert = 1;
	SignPara.rgpMsgCert = &pSignerCertContext ;
	SignPara.cMsgCrl = 0;
	SignPara.rgpMsgCrl = NULL;
	SignPara.cAuthAttr = 0;
	SignPara.rgAuthAttr = NULL;
	SignPara.cUnauthAttr = 0;
	SignPara.rgUnauthAttr = NULL;
	SignPara.dwFlags = 0;
	SignPara.dwInnerContentType = 0;

	EncryptPara.cbSize = sizeof(CRYPT_ENCRYPT_MESSAGE_PARA);
	EncryptPara.dwMsgEncodingType = MY_TYPE;
	EncryptPara.hCryptProv = 0;
	EncryptPara.ContentEncryptionAlgorithm.pszObjId = szOID_RSA_RC4;
	EncryptPara.ContentEncryptionAlgorithm.Parameters.cbData = 0;
	EncryptPara.pvEncryptionAuxInfo = NULL;
	EncryptPara.dwFlags = 0;
	EncryptPara.dwInnerContentType = 0;

	cRecipientCert = 1;
	rgpRecipientCert[0] = pReceiverCertContext;
	*pcbSignedAndEncryptedBlob = 0;
	pbSignedAndEncryptedBlob = NULL;

	if( CryptSignAndEncryptMessage(
		&SignPara,
		&EncryptPara,
		cRecipientCert,
		rgpRecipientCert,
		pbToBeSignedAndEncrypted,
		cbToBeSignedAndEncrypted,
		NULL,                      // the pbSignedAndEncryptedBlob
		pcbSignedAndEncryptedBlob))
	{
		printf("%d bytes for the buffer .\n",*pcbSignedAndEncryptedBlob);
	}
	else
	{
		HandleError("Getting the buffer length failed.");
	}

	//--------------------------------------------------------------------
	//    Allocated memory for the buffer

	if(!(pbSignedAndEncryptedBlob=(unsigned char *)
		malloc(*pcbSignedAndEncryptedBlob)))
		HandleError("Memory allocation failed.");

	//--------------------------------------------------------------------
	//   Call the function a second time to copy the signed and encrypted
	//   message into the buffer.

	if( CryptSignAndEncryptMessage(
		&SignPara,
		&EncryptPara,
		cRecipientCert,
		rgpRecipientCert,
		pbToBeSignedAndEncrypted,
		cbToBeSignedAndEncrypted,
		pbSignedAndEncryptedBlob,
		pcbSignedAndEncryptedBlob))
	{
		printf("The message is signed and encrypted.\n");
	}
	else
	{
		HandleError("The message failed to sign and encrypt.");
	}

	//--------------------------------------------------------------------
	//   Clean up.

	if(pSignerCertContext )
	{
		CertFreeCertificateContext (pSignerCertContext );
	}
	if(pReceiverCertContext )
	{
		CertFreeCertificateContext (pReceiverCertContext );
	}
	CertCloseStore(
		hCertStore,
		0);

	//--------------------------------------------------------------------
	//   Return the signed and encrypted message.

	return pbSignedAndEncryptedBlob;

}  // End SignandEncrypt.

//--------------------------------------------------------------------
//   Define the DecryptAndVerify function.

BYTE  *DecryptAndVerify(
						BYTE  *pbSignedAndEncryptedBlob,
						DWORD  cbSignedAndEncryptedBlob)
{

	//--------------------------------------------------------------------
	//  Declare and initialize local variables.

	HCERTSTORE                     hCertStore;
	CRYPT_DECRYPT_MESSAGE_PARA     DecryptPara;
	CRYPT_VERIFY_MESSAGE_PARA      VerifyPara;
	DWORD                          dwSignerIndex = 0;
	BYTE                           *pbDecrypted;
	DWORD                          cbDecrypted;

	//--------------------------------------------------------------------
	//   Open the certificate store

	if ( !( hCertStore = CertOpenStore(
		CERT_STORE_PROV_SYSTEM,
		0,
		NULL,
		CERT_SYSTEM_STORE_CURRENT_USER,
		L"my")))
	{
		HandleError("The MY store could not be openned.");
	}

	//--------------------------------------------------------------------
	//   Initialize the needed data structures.

	DecryptPara.cbSize = sizeof(CRYPT_DECRYPT_MESSAGE_PARA);
	DecryptPara.dwMsgAndCertEncodingType = MY_TYPE;
	DecryptPara.cCertStore = 1;
	DecryptPara.rghCertStore = &hCertStore;

	VerifyPara.cbSize = sizeof(CRYPT_VERIFY_MESSAGE_PARA);
	VerifyPara.dwMsgAndCertEncodingType = MY_TYPE;
	VerifyPara.hCryptProv = 0;
	VerifyPara.pfnGetSignerCertificate = NULL;
	VerifyPara.pvGetArg = NULL;
	pbDecrypted = NULL;
	cbDecrypted = 0;

	//--------------------------------------------------------------------
	//     Call CryptDecryptAndVerifyMessageSignature a first time
	//     to determine the needed size of the buffer to hold the 
	//     decrypted message.

	if(!(CryptDecryptAndVerifyMessageSignature(
		&DecryptPara,
		&VerifyPara,
		dwSignerIndex,
		pbSignedAndEncryptedBlob,
		cbSignedAndEncryptedBlob,
		NULL,           // pbDecrypted
		&cbDecrypted,
		NULL,
		NULL)))
	{
		HandleError("Failed getting size.");
	}

	//--------------------------------------------------------------------
	//    Allocate memory for the buffer to hold the decrypted message.

	if(!(pbDecrypted = (BYTE *)malloc(cbDecrypted)))
		HandleError("Memory allocation failed.");

	if(!(CryptDecryptAndVerifyMessageSignature(
		&DecryptPara,
		&VerifyPara,
		dwSignerIndex,
		pbSignedAndEncryptedBlob,
		cbSignedAndEncryptedBlob,
		pbDecrypted,
		&cbDecrypted,
		NULL,
		NULL)))
	{
		pbDecrypted = NULL;
	}

	//--------------------------------------------------------------------
	//  Close the certificate store.

	CertCloseStore(
		hCertStore,
		0);

	//--------------------------------------------------------------------
	//    Return the decrypted string or NULL

	return pbDecrypted;

} // end of DecryptandVerify

//--------------------------------------------------------------------
//   Define the HandleError function.

void WriteSignedAndEncryptedBlob(
								 DWORD  cbBlob,
								 BYTE   *pbBlob)
{
	// Open an output file, write the file, and close the file.
	// This function would be used to save the signed and encrypted 
	// message to a file that would be sent to the intended receiver.
	// Note: the only receiver able to decrypt and verify this message 
	// will have access to the private key associated 
	// with the public key from the certificate used when the message was
	// encrypted.

	FILE *hOutputFile;

	if( !(hOutputFile= fopen("sandvout.txt","wb")))
	{
		HandleError("Output file was not opened.\n");
	}

	fwrite(
		&cbBlob,
		sizeof(DWORD),
		1,
		hOutputFile);

	if(ferror(hOutputFile))
	{
		HandleError("The size of the BLOB was not written.\n");
	}


	fwrite(
		pbBlob,
		cbBlob,
		1,
		hOutputFile);

	if(ferror(hOutputFile))
	{
		HandleError("The bytes of the BLOB were not written.\n");
	}
	else
	{
		printf(" The BLOB has been written to the file.\n");
	}
	fclose(hOutputFile);
}  // end of WriteSignedAndEcryptedBlob


void HandleError(char *s)
{
	printf("%s\n",s);
	exit(1);
} // end of HandleError

//--------------------------------------------------------------------
//  Define the ShowBytes function.
//  This function displays the contents of a BYTE buffer. Characters
//  less than '0' or greater than 'z' are all displayed as '-'.

void ShowBytes(BYTE *s, DWORD len)
{
	DWORD TotalChars = 0;
	DWORD ThisLine = 0;
	while(TotalChars < len)
	{
		if(ThisLine > 70)
		{
			ThisLine = 0;
			printf("\n");
		}
		if( s[TotalChars] < '0' || s[TotalChars] > 'z')
		{
			printf("-");
		}
		else
		{
			printf("%c",s[TotalChars]);
		}
		TotalChars++;
		ThisLine++;
	}
	printf("\n");
} // End of ShowBytes.
