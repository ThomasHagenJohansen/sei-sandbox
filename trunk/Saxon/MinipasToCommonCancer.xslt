<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema">
	<xsl:output method="xml" indent="yes" media-type="text/xml" />
	
	<!-- ICD10-koder der beskriver cancer -->
	<xsl:variable name="cancer_20040101_20040630">(DC[0136-8]\d\d?|DC2[0-6]\d?|DC4[013-9]\d?|DC5[0-8]\d?|DC9[0-7]\d?|DD3[0237-9]\d?|DD4[0-8]\d?|DD06\d?|DN87\d?|DO01\d?|DB21\d?|DD09[01]|DD35[2-4]|DE340|DD076)[A-Z]?</xsl:variable>
	<xsl:variable name="cancer_20040701_">(DC[0136-8]\d\d?|DC2[0-6]\d?|DC4[013-9]\d?|DC5[0-8]\d?|DC9[0-7]\d?|DD06\d?|DD3[023]\d?|DB21\d?|DD09[01]|DD076)[A-Z]?</xsl:variable>
	
	<!-- ICD10-koder der kræver lateralitet -->
	<xsl:variable name="icd10_lateralitet_20040101_">(DC08\d?|DC[367]4\d?|DC[45]0\d?|DC[56]6\d?|DC6[259]\d?|DC079|D[CD]301|DC384|DC4[34][267]|DC450|DC4[79][12]|DC57[0-4]|DC63[01]|DC76[45]|DC78[02]|DC79[067]|DD30[02]|DD38[12]|DD391|DD4[014]1|DD41[02]|DD486)[A-Z]?</xsl:variable>
	
	<!-- ICD10-koder der kræver TNM -->
	<xsl:variable name="icd10_tnm_20040101_">(DC[0-6]\d\d?|DC7[3-9]\d?|DC80\d?|DC902|DC923|DC9[67]\d?|DB21\d?|DD06\d?|DD076|DD09[01]|DD3[07-9]\d?|DD4[0148]\d?|DE340)[A-Z]?</xsl:variable>
	
	<!-- ICD10-koder der kræver ANNARBOR -->
	<xsl:variable name="icd10_annarbor_20040101_">(DC81*-DC85*)[A-Z]?</xsl:variable>
	
	<!-- Prioriteret liste af Ann Arbor koder -->
	<xsl:variable name="annarbor_codes_pri" select="reverse(('AZCC99','AZCC1','AZCC1A','AZCC1B','AZCC2','AZCC2A','AZCC2B','AZCC3','AZCC3A','AZCC3B','AZCC4','AZCC4A','AZCC4B'))" />
	
	<!-- Prioriteret liste af T koder -->
	<xsl:variable name="t_codes_pri" select="reverse(('AZCD19','AZCD10','AZCD11','AZCD12','AZCD13','AZCD13A','AZCD13A1','AZCD13A2','AZCD13B','AZCD13B1','AZCD13B2','AZCD13C','AZCD14','AZCD14A','AZCD14B','AZCD14C','AZCD15','AZCD15A','AZCD15B','AZCD15C','AZCD16','AZCD16A','AZCD16B','AZCD16C','AZCD16D'))" />
	
	<!-- Prioriteret liste af N koder -->
	<xsl:variable name="n_codes_pri" select="reverse(('AZCD39','AZCD30','AZCD31','AZCD31A','AZCD31B','AZCD31C','AZCD32','AZCD32A','AZCD32B','AZCD32C','AZCD33','AZCD33A','AZCD33B','AZCD33C'))" />
	
	<!-- Prioriteret liste af M koder -->
	<xsl:variable name="m_codes_pri" select="reverse(('AZCD49','AZCD40','AZCD41','AZCD41A','AZCD41B','AZCD41C'))" />
	
	<xsl:template match="MiniPasDS|NewDataSet">
		<xsl:variable name="skemaid" select="//MiniPas_Skema1_GrundOplysninger/uiSkemaID"/>
		
		<!-- Find incidensdato -->
		<xsl:variable name="incidensdato" as="xs:dateTime"><xsl:call-template name="get_incidensdato" /></xsl:variable>
	
		<!-- Brug de rette ICD10-koder -->				
		<xsl:variable name="cancer_codes">
			<xsl:choose>
				<xsl:when test="xs:date($incidensdato) lt xs:date('2004-07-01')"><xsl:value-of select="$cancer_20040101_20040630"/></xsl:when>
				<xsl:when test="xs:date($incidensdato) gt xs:date('2004-06-30')"><xsl:value-of select="$cancer_20040701_"/></xsl:when>
			</xsl:choose>
		</xsl:variable>
	
		<CommonCancerSchema xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation='CommonCancer.xsd'>
			<xsl:call-template name="process_code_add">
				<xsl:with-param name="code"><xsl:value-of select="//MiniPas_Skema1_GrundOplysninger/txDAktionsdiagnoseKode"/></xsl:with-param>
				<xsl:with-param name="table" select="//MiniPas_Skema1_DiagnoseKoder_ATillaeg[uiSkemaID=$skemaid]" />
				<xsl:with-param name="validcodes" select="$cancer_codes" />
				<xsl:with-param name="incidensdato" select="$incidensdato"/>
			</xsl:call-template>
			<xsl:for-each select="MiniPas_Skema1_DiagnoseKoder_Bidiag">
				<xsl:variable name="lineid" select="uiLinesID"/>
				<xsl:call-template name="process_code_add">
					<xsl:with-param name="code"><xsl:value-of select="txBidiagnoseKode"/></xsl:with-param>
					<xsl:with-param name="table" select="//MiniPas_Skema1_DiagnoseKoder_BTillaeg[uiParentID=$lineid]" />
					<xsl:with-param name="validcodes" select="$cancer_codes" />
					<xsl:with-param name="incidensdato" select="$incidensdato"/>
				</xsl:call-template>
			</xsl:for-each>
		</CommonCancerSchema>
	</xsl:template>
	
	<!-- Generer en CommonCancer record -->
	
	<xsl:template name="process_code_add">
		<xsl:param name="code"/>
		<xsl:param name="table"/>
		<xsl:param name="validcodes"/>
		<xsl:param name="incidensdato"/>
		<xsl:if test="matches($code, $validcodes)">
			<xsl:variable name="subcodes" select="$table/(txBiTillaegsKode|txAktionsTillaegsKode)" />
			
			<xsl:if test="some $x in $subcodes satisfies $x = 'AZCA1'">
				<CommonCancer>
					<uiSkemaId><xsl:value-of select="//MiniPas_Skema1_GrundOplysninger/uiSkemaID"/></uiSkemaId>
					<txCprNr><xsl:value-of select="//MiniPas_Skema1_GrundOplysninger/txCPR"/></txCprNr>
					<dtIncidensdato><xsl:value-of select="$incidensdato"/></dtIncidensdato>
					<txDiagnosekode><xsl:value-of select="substring($code,1,5)"/></txDiagnosekode>
					<xsl:for-each select="$subcodes">
						<xsl:if test="matches( text(), 'TUL[123]' )">
							<txLateralitet><xsl:value-of select="text()"/></txLateralitet>
						</xsl:if>
					</xsl:for-each>
					<xsl:choose>
						<xsl:when test="matches( $code, $icd10_annarbor_20040101_ )">
							<txAA>
								<xsl:call-template name="find_first">
									<xsl:with-param name="list" select="$annarbor_codes_pri"/>
									<xsl:with-param name="nodes" select="$subcodes"/>
								</xsl:call-template>
							</txAA>
						</xsl:when>
						<xsl:when test="matches( $code, $icd10_tnm_20040101_ )">
							<txT>
								<xsl:call-template name="find_first">
									<xsl:with-param name="list" select="$t_codes_pri"/>
									<xsl:with-param name="nodes" select="$subcodes"/>
								</xsl:call-template>
							</txT>
							<txN>
								<xsl:call-template name="find_first">
									<xsl:with-param name="list" select="$n_codes_pri"/>
									<xsl:with-param name="nodes" select="$subcodes"/>
								</xsl:call-template>
							</txN>
							<txM>
								<xsl:call-template name="find_first">
									<xsl:with-param name="list" select="$m_codes_pri"/>
									<xsl:with-param name="nodes" select="$subcodes"/>
								</xsl:call-template>
							</txM>
						</xsl:when>
					</xsl:choose>
					<xsl:for-each select="$subcodes">
						<xsl:if test="matches( text(), 'AZCK\d' )">
							<txMakroGrundlag><xsl:value-of select="text()"/></txMakroGrundlag>
						</xsl:if>
					</xsl:for-each>
					<xsl:for-each select="$subcodes">
						<xsl:if test="matches( text(), 'AZCL\d' )">
							<txMikroGrundlag><xsl:value-of select="text()"/></txMikroGrundlag>
						</xsl:if>
					</xsl:for-each>
				</CommonCancer>
			</xsl:if>
		</xsl:if>
	</xsl:template>
	
	<!-- Returner den første forekomst i nodes af et af lists elementer. -->
	
	<xsl:template name="find_first">
		<xsl:param name="list" />
		<xsl:param name="nodes" />
		<xsl:choose>
			<xsl:when test="some $x in $nodes satisfies string($x) = string($list[1])">
				<xsl:value-of select="$list[1]" />
			</xsl:when>
			<xsl:when test="count($list) gt 1">
				<xsl:call-template name="find_first">
					<xsl:with-param name="list" select="remove($list,1)"/>
					<xsl:with-param name="nodes" select="$nodes"/>
				</xsl:call-template>
			</xsl:when>
		</xsl:choose>
	</xsl:template>

	<!-- Find incidensdato -->
	
	<xsl:template name="get_incidensdato">
		<xsl:choose>
			<xsl:when test="//MiniPas_Skema1_GrundOplysninger/iPatientType = 0">
				<!-- Indlagt patient -->
				<xsl:choose>
					<xsl:when test="//MiniPas_Skema1_GrundOplysninger/dtIHenvisningsDato != '' and (substring(//MiniPas_Skema1_GrundOplysninger/dtIHenvisningsDato,1,11) = '0001-01-01')">
						<xsl:value-of select="//MiniPas_Skema1_GrundOplysninger/dtIHenvisningsDato"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="//MiniPas_Skema1_GrundOplysninger/dtIIndlaeggelsesDato"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			
			<xsl:when test="//MiniPas_Skema1_GrundOplysninger/iPatientType > 0">
				<!-- Ambulant patient -->
				<!-- Hvis ambulant henvisningsdato er tom, find første besøgsdato -->
				<xsl:choose>
					<xsl:when test="(//MiniPas_Skema1_GrundOplysninger/dtAHenvisningsDato != '') and (substring(//MiniPas_Skema1_GrundOplysninger/dtAHenvisningsDato,1,11) = '0001-01-01')">
						<xsl:value-of select="//MiniPas_Skema1_GrundOplysninger/dtAHenvisningsDato"/>
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="min(for $x in //MiniPas_Skema1_Ambulant_BesoegLines/dtBesoegsDato return xs:dateTime($x))" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
