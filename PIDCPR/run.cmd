set JAVA_HOME=C:\j2sdk1.4.2_02



SET AXIS_HOME=.\apache\axis\1.1\lib
set CLASSPATH=./build
set CLASSPATH=%CLASSPATH%;%AXIS_HOME%\axis.jar
set CLASSPATH=%CLASSPATH%;%AXIS_HOME%\axis-ant.jar
set CLASSPATH=%CLASSPATH%;%AXIS_HOME%\commons-discovery.jar
set CLASSPATH=%CLASSPATH%;%AXIS_HOME%\commons-logging.jar
set CLASSPATH=%CLASSPATH%;%AXIS_HOME%\jaxrpc.jar
set CLASSPATH=%CLASSPATH%;%AXIS_HOME%\log4j-1.2.8.jar
set CLASSPATH=%CLASSPATH%;%AXIS_HOME%\saaj.jar
set CLASSPATH=%CLASSPATH%;%AXIS_HOME%\wsdl4j.jar


set JAVA_OPTIONS=-ms64m 
set JAVA_OPTIONS=%JAVA_OPTIONS% -mx64m
set JAVA_OPTIONS=%JAVA_OPTIONS% -classpath %CLASSPATH%
set JAVA_OPTIONS=%JAVA_OPTIONS% -Djavax.net.ssl.trustStore=tdcssl.jks 
set JAVA_OPTIONS=%JAVA_OPTIONS% -Djavax.net.ssl.trustStorePassword=gnyffe
set JAVA_OPTIONS=%JAVA_OPTIONS% -Djavax.net.ssl.keyStore=PIDTest.jks 
set JAVA_OPTIONS=%JAVA_OPTIONS% -Djavax.net.ssl.keyStorePassword=Test1234
echo on

echo "using JAVA OPTIONS : "%JAVA_OPTIONS%

"%JAVA_HOME%\bin\java" %JAVA_OPTIONS%  AxisWSClient

goto finish
:finish
ENDLOCAL




