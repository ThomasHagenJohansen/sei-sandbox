<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" indent="yes" media-type="text/xml" />
	<xsl:template match="Skema1">
		<CommonCancerSchema xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation='CommonCancer.xsd'>
			<CommonCancer>
				<txCprNr><xsl:value-of select="txCPR"/></txCprNr>
				<dtIncidensdato><xsl:value-of select="dtFoersteKontakt"/></dtIncidensdato>
				<txDiagnosekode><xsl:value-of select="txDiagnoseKode"/></txDiagnosekode>
				<xsl:if test="txLateralitet != ''">
					<txLateralitet><xsl:value-of select="txLateralitet"/></txLateralitet>
				</xsl:if>
				<xsl:choose>
					<xsl:when test="txUdbredelsesFormat = 'ANNARBOR'">
						<!-- <txUdbredelsesformat>AnnArbor</txUdbredelsesformat> -->
						<txAA><xsl:value-of select="txAA"/></txAA>
					</xsl:when>
					<xsl:when test="txUdbredelsesFormat = 'TNM'">
						<!-- <txUdbredelsesformat>TNM</txUdbredelsesformat> -->
						<txT><xsl:value-of select="txT"/></txT>
						<txN><xsl:value-of select="txN"/></txN>
						<txM><xsl:value-of select="txM"/></txM>
					</xsl:when>
					<!-- Hvis ingen af de to formater, skippes udbredelse helt -->
				</xsl:choose>
				<xsl:call-template name="stringsplit">
					<xsl:with-param name="str" select="txDiagnoseGrundlagKoder" />
					<xsl:with-param name="tag">txMakroGrundlag</xsl:with-param>
					<xsl:with-param name="filter">AZCK</xsl:with-param>
				</xsl:call-template>
				<xsl:call-template name="stringsplit">
					<xsl:with-param name="str" select="txDiagnoseGrundlagKoder" />
					<xsl:with-param name="tag">txMikroGrundlag</xsl:with-param>
					<xsl:with-param name="filter">AZCL</xsl:with-param>
				</xsl:call-template>
			</CommonCancer>
		</CommonCancerSchema>
	</xsl:template>

	<xsl:template name="stringsplit">
		<xsl:param name="str"/>
		<xsl:param name="tag"/>
		<xsl:param name="filter"/>
		<xsl:choose>
			<xsl:when test="contains($str,'+')">
				<xsl:call-template name="writetag">
					<xsl:with-param name="str" select="substring-before($str,'+')" />
					<xsl:with-param name="tag" select="$tag" />
					<xsl:with-param name="filter" select="$filter" />
				</xsl:call-template>
				<xsl:call-template name="stringsplit">
					<xsl:with-param name="str" select="substring-after($str,'+')" />
					<xsl:with-param name="tag" select="$tag" />
					<xsl:with-param name="filter" select="$filter" />
				</xsl:call-template>
			</xsl:when>
			<xsl:otherwise>
				<xsl:call-template name="writetag">
					<xsl:with-param name="str" select="$str" />
					<xsl:with-param name="tag" select="$tag" />
					<xsl:with-param name="filter" select="$filter" />
				</xsl:call-template>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	
	<!-- Generer et $tag-tag med værdien $str, hvis $str starter med $filter -->
	
	<xsl:template name="writetag">
		<xsl:param name="str"/>
		<xsl:param name="tag"/>
		<xsl:param name="filter"/>
		<xsl:if test="starts-with($str,$filter)">
			<xsl:element name="{$tag}"><xsl:value-of select="$str"/></xsl:element>
		</xsl:if>
	</xsl:template>

</xsl:stylesheet>
