set JAVA_HOME=C:\j2sdk1.4.2_02

set CLASSPATH=

set ANT_HOME=.\apache\ant\1.5.4
set JAVA_OPTIONS=-Djavax.net.ssl.trustStore=tdcssl.jks 
set JAVA_OPTIONS=%JAVA_OPTIONS% -Djavax.net.ssl.trustStorePassword=gnyffe
set JAVA_OPTIONS=%JAVA_OPTIONS% -Djavax.net.ssl.keyStore=PIDTest.jks 
set JAVA_OPTIONS=%JAVA_OPTIONS% -Djavax.net.ssl.keyStorePassword=Test1234

set ANT_OPTS=%JAVA_OPTIONS%

echo building axis ws client

%ANT_HOME%\bin\ant -buildfile wsaxis-build.xml






