package dk.certifikat.pid.client;

import localhost.PidwsLocator;
import webservices.pid.certifikat.dk.PIDReply;
import webservices.pid.certifikat.dk.PIDRequest;

import java.net.URL;


public class AxisWSClient {


    public static void main(String[] args) throws Exception {

        final String SERVICE_ENDPOINT_HTTPS = "https://test.pid.certifikat.dk/pidwsv2/pidws?WSDL";

        String b64Cert = "MIIEJzCCAw+gAwIBAgIEPHEWCjANBgkqhkiG9w0BAQUFADBLMQswCQYDVQQGEwJESzEVMBMGA1UEChMMVERDIEludGVybmV0MSUwIwYDVQQLExxUREMgSW50ZXJuZXQgU3lzdGVtdGVzdCBDQSBJMB4XDTAyMDgyMjA2MjQzNVoXDTAzMDgyMjA2NTQzNVowejELMAkGA1UEBhMCREsxKTAnBgNVBAoTIEluZ2VuIE9yZ2FuaXNhdG9yaXNrIFRpbGtueXRuaW5nMUAwGQYDVQQDExJQZXR0ZXIgS3ZhbGlmIEJ1dXMwIwYDVQQFExxQSUQ6OTgwMi0yMDAyLTItNDAyOTE4NDI0NTMzMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDD0OkKeEsEFPzLaGEur9w56eGcR72w4h4nq4qEzGauaLfikixsrxmXHgQbxMcXFiCG43TUcIhHBnMKxFOI3rdN1Yk5fakgIL47/DfzMU+rxrjNUFBzuSQvRfGX3HF88g5w0WTELXH4Xq/00L/4+j/yQYt+5AtW4tsfhIBCLNAXiwIDAQABo4IBZjCCAWIwCwYDVR0PBAQDAgbAMCsGA1UdEAQkMCKADzIwMDIwODIyMDYyNDM1WoEPMjAwMzA4MjIwNjU0MzVaMB4GA1UdEQQXMBWBE3BidXVAdGRjaW50ZXJuZXQuZGswgZ8GA1UdHwSBlzCBlDBioGCgXqRcMFoxCzAJBgNVBAYTAkRLMRUwEwYDVQQKEwxUREMgSW50ZXJuZXQxJTAjBgNVBAsTHFREQyBJbnRlcm5ldCBTeXN0ZW10ZXN0IENBIEkxDTALBgNVBAMTBENSTDEwLqAsoCqGKGh0dHA6Ly9yaW1mYWtzZS5jZXJ0aWZpa2F0LmRrL3N0ZXN0MS5jcmwwHwYDVR0jBBgwFoAUSWYFHUgAHH5hVZHqd+HQCtsPx/swHQYDVR0OBBYEFEUyHHZ8xpr6uixg8UCA6wyd+noZMAkGA1UdEwQCMAAwGQYJKoZIhvZ9B0EABAwwChsEVjYuMAMCA6gwDQYJKoZIhvcNAQEFBQADggEBAFb8iy4BKAaxcgLJhhexhA5Lv23YtWpbMRU1OoDvWqcKUBo+wBH6xAFPkzRkiI8CcNKvSs+LBdqJb//z/bMjvvElfXkbziVOSCjNxIYu0+FbH+ygEso1jA11NoCPhSlSRrhQGKUduvmTLv31qMxBncgdnr+1J0LNM9IQM5VB7tgBkdetF5Afeev6wx+8vdg7SCODMSQW4NS3OWngdONZwblNNc4Z0kO293BQQyPgbIbXQbNsUI6yaQpXJT2YW7TVQRDo1Vu9OZXUmYAQ968DC2NdShg16t29dxb5I/IHJctX+ZDu+RxALeAhpoh6MW6tBGOYgXg1subMHEP44GAOAB4=";

        System.out.println("Starting ..........");

        PidwsLocator locator = new PidwsLocator();
        localhost.PidwsPort service = locator.getpidwsPort(new URL(SERVICE_ENDPOINT_HTTPS));

        System.out.println("Service looked up.");

        // build PIDRequest
        PIDRequest[] request = new PIDRequest[3];

        request[0] = new PIDRequest();
        request[0].setId("0000");
        request[0].setServiceId("133");
        request[0].setPID("PID:9802-2002-2-847237306513");

        request[1] = new PIDRequest();
        request[1].setId("0001");
        request[1].setServiceId("133");
        request[1].setCPR("2802751799");
        request[1].setCPR("2802751798");
        request[1].setPID("PID:9802-2002-2-847237306513");

        request[2] = new PIDRequest();
        request[2].setId("0002");
        request[2].setServiceId("133");
        request[2].setB64Cert(b64Cert);

        // call webservice method pid
        System.out.println("About to call WebService.");
        PIDReply[] reply = null;
        reply = service.pid(request);

        System.out.println("WebService called length " + reply.length);

        // print response
        System.out.println("0 Response id: " + reply[0].getId());
        System.out.println("0 Status code: " + reply[0].getStatusCode());
        System.out.println("0 Status text DK: " + reply[0].getStatusTextDK());
        System.out.println("0 Status text UK: " + reply[0].getStatusTextUK());

        System.out.println("");

        System.out.println("1 Response id: " + reply[1].getId());
        System.out.println("1 Status code: " + reply[1].getStatusCode());
        System.out.println("1 Status text DK: " + reply[1].getStatusTextDK());
        System.out.println("1 Status text UK: " + reply[1].getStatusTextUK());
        System.out.println("1 CPR: " + reply[1].getCPR());

        System.out.println("");

        System.out.println("2 Response id: " + reply[2].getId());
        System.out.println("2 Status code: " + reply[2].getStatusCode());
        System.out.println("2 Status text DK: " + reply[2].getStatusTextDK());
        System.out.println("2 Status text UK: " + reply[2].getStatusTextUK());
        System.out.println("2 CPR: " + reply[2].getCPR());

    }
}
