namespace TaskBoards.Services
{
    //You can use this key to identify your requests.
    //Public Key:
    //96d3cde22acce4e26d5b0e2c5f892b32

    //You can use this secret to sign your requests.
    //Secret:
    //910a083eccfdd1a73843bd217854fe1a9bf322d80249e92a70648ab8050e808d

    //***********TESTING*******
    //Key: 97629f303143220579cfaf4cb33c4fa7
    //Secret (for OAuth signing): 869dd4caaa5104a66a0d88a4fc9775523793bc35fb49df4202696ccbde1abb2c

    public class TrelloApi
    {
        //Prod
        public static string Key = "96d3cde22acce4e26d5b0e2c5f892b32"; 
        public const string ServiceBaseUrl = "https://api.trello.com/1";
        public const string Uri = "https://trello.com/1/authorize?key=96d3cde22acce4e26d5b0e2c5f892b32%26" +
                  "name=Taskboards%20for%20WP7%26" +
                  "response_type=token%26" +
                  "scope=read,write%26" +
                  "expires=never";

        //Testing
        //public const string ServiceBaseUrl = "http://api.test.trello.org/1";
        //public static string Key = "97629f303143220579cfaf4cb33c4fa7"; 
        //public const string Uri = "http://test.trello.org/1/authorize?key=97629f303143220579cfaf4cb33c4fa7%26" +
        //                   "name=Taskboards%20for%20WP7%26" +
        //                   "response_type=token%26" +
        //                   "scope=read,write%26" +
        //                   "expires=never";
    }
}