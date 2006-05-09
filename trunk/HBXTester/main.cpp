#include "CHeapReplacement.h"
#include <Windows.h>
#include "CThread.h"
#include "CFileMatcher.h"
#include "Interface.h"
#include "resource.h"
#include "TList.h"

const TCHAR *g_pFiles[]=
{
	_T("dk.hob.SSTCancer.T.hbx"),
	_T("dk.hob.SSTCancer.N.hbx"),
	_T("dk.hob.SSTCancer.M.hbx"),
	_T("dk.hob.SSTCancer.AnnArbor.hbx"),
	_T("ICD10.sst.hbx"),
	_T("dk.hob.SSTCancer.Radikalitet.hbx"),
	_T("dk.hob.SSTCancer.OperationsKoder.hbx"),
	_T("dk.hob.SSTCancer.AnmeldelsesPligtige.hbx")
};

class CMyThread : public CThread
{
public:
	CMyThread( HINSTANCE hInst ) : CThread(0)
	{
		m_hInst=hInst;
		m_hDialog=NULL;
	}
	virtual ~CMyThread()
	{
		for( int i=0; i<m_Files.Size(); ++i )
			delete[] m_Files[i];

		m_Files.Clear();
	}

	virtual void Run()
	{
		::DialogBoxParam( m_hInst, MAKEINTRESOURCE(IDD_DIALOG), NULL, MyDialogProc, (LPARAM)this );
	}

	virtual bool Running()
	{
		return m_hDialog!=NULL;
	}

	virtual void AddFileToList( const TCHAR *pFile, const TCHAR *pContent )
	{
		HWND h=GetDlgItem( m_hDialog, IDC_LISTFILES );
		SendMessage( h, LB_ADDSTRING, 0, (LPARAM)pFile );
		TCHAR *t=new TCHAR[_tcslen(pContent)+1];
		memcpy( t, pContent, (_tcslen(pContent)+1)*sizeof(TCHAR) );
		m_Files.Add( t );
	}

private:
	static INT_PTR CALLBACK MyDialogProc( HWND hwndDlg, UINT uMsg, WPARAM wParam, LPARAM lParam )
	{
		switch( uMsg )
		{
			case WM_INITDIALOG:
			{
				CMyThread *p=(CMyThread *)lParam;
				p->m_hDialog=hwndDlg;
				SetWindowLongPtr( hwndDlg, GWLP_USERDATA, (LONG)p );
				return TRUE;
			}
			case WM_COMMAND:
			{
				CMyThread *p=(CMyThread *)GetWindowLongPtr( hwndDlg, GWLP_USERDATA );
				int id=LOWORD(wParam);
				int msg=HIWORD(wParam);
				switch( id )
				{
					case IDCANCEL:
					case IDOK:
					{
						::EndDialog( p->m_hDialog, 0 );
						return TRUE;
					}
					case IDC_LISTFILES:
					{
						switch( msg )
						{
							case LBN_SELCHANGE:
							{
								int index=SendMessage( (HWND)lParam, LB_GETCURSEL, 0, 0 );
								SetWindowText( GetDlgItem(hwndDlg,IDC_EDITFILE), p->m_Files[index] );
								return TRUE;
							}
						}
					}
				}
			}
		}
		return FALSE;
	}

	volatile HWND	m_hDialog;
	HINSTANCE		m_hInst;
	TList<TCHAR *>	m_Files;
};

void ProcessFile( CMyThread *th, const CUniString &s )
{
	TList<TCHAR> content;
	TCHAR *t=s.to_tchar();
	HANDLE h=::CreateFile( t, GENERIC_READ, FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0 );
	HBXF_Context ctx=::HBXF_NewFileReader( h );
	bool needclose=false;
	while( ::HBXF_Read( ctx ) )
	{
		switch( ::HBXF_GetNodeType(ctx) )
		{
			case NODE_ELEMENT:
			{
				if( needclose )
				{
					content.Add( _T('>') );
					content.Add( _T('\r') );
					content.Add( _T('\n') );
					needclose=false;
				}

				const wchar_t *p=::HBXF_GetName(ctx);
				content.Add( _T('<') );
				while( *p )
					content.Add( TCHAR(*p++) );
				needclose=true;
				break;
			}
			case NODE_ATTRIBUTE:
			{
				const wchar_t *p=::HBXF_GetName(ctx);
				while( *p )
					content.Add( TCHAR(*p++) );

				content.Add( _T('=') );
				content.Add( _T('\'') );

				p=::HBXF_GetValue(ctx);
				while( *p )
					content.Add( TCHAR(*p++) );

				content.Add( _T('\'') );
				content.Add( _T(' ') );
				break;
			}
			case NODE_TEXT:
			{
				if( needclose )
				{
					content.Add( _T('>') );
					needclose=false;
				}

				const wchar_t *p=::HBXF_GetValue(ctx);
				while( *p )
					content.Add( TCHAR(*p++) );
				break;
			}
			case NODE_ENDELEMENT:
			{
				if( needclose )
				{
					content.Add( _T('>') );
					content.Add( _T('\r') );
					content.Add( _T('\n') );
					needclose=false;
				}

				const wchar_t *p=::HBXF_GetName(ctx);
				content.Add( _T('<') );
				content.Add( _T('/') );
				while( *p )
					content.Add( TCHAR(*p++) );
				content.Add( _T('>') );
				content.Add( _T('\r') );
				content.Add( _T('\n') );
				break;
			}
		}
	}
	content.Add( 0 );
	th->AddFileToList( t, &content[0] );

	::HBXF_Free( ctx );
	CloseHandle( h );
	delete[] t;
}

int __stdcall WinMain( HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow )
{
	CMyThread *th=new CMyThread( hInstance );

	th->Start();
	while( !th->Running() )
		Sleep( 50 );

	/*
	CFileMatcher *fm=new CFileMatcher( L"D:\\HOB-SVN-Build\\EI\\SST\\Client\\SST\\bin\\Data\\*.hbx" );
	//CFileMatcher *fm=new CFileMatcher( L"D:\\HOB-SVN-Build\\EI\\SST\\Client\\SST\\bin\\Data\\Postnr.hbx" );
	CUniString s;
	while( (s=fm->Next())!=CUniString::null )
	{
		ProcessFile( s );
	}
	delete fm;
	*/

	for( int i=0; i<sizeof(g_pFiles)/sizeof(const TCHAR *); ++i )
	{
		ProcessFile( th, CUniString(L"D:\\HOB-SVN-Build\\EI\\SST\\Client\\SST\\bin\\Data\\")+g_pFiles[i] );
	}

	th->WaitForEnd();
	delete th;

	return 0;
}
