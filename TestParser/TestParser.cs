using System;
using Parser;


namespace TestParser;

[TestClass]
public class TestParser
{   
    string exampleFile = @"../../../../test_config.ini";


    [TestMethod]
    public void TestParserConfigGetSections()
    {   
        ParserConfig parser = new ParserConfig(exampleFile);
        string[] expected_output = {"rest_config", "middleware", "mqtt", "cookies"};
        CollectionAssert.AreEqual(expected_output, parser.GetSections());
    }

    [TestMethod]
    public void TestParserConfigGetValue()
    {   
        ParserConfig parser = new ParserConfig(exampleFile);
        Assert.AreEqual("127.0.0.1", parser.GetValue("rest_config", "server_address"));
        Assert.AreEqual("example_password", parser.GetValue("rest_config", "server_password"));
    }

    [TestMethod]
    public void TestParserConfigGetValueFallback()
    {   
        ParserConfig parser = new ParserConfig(exampleFile);
        Assert.AreEqual("", parser.GetValue("rest_config", "non-existing-value"));
        Assert.AreEqual("non-existing-value", parser.GetValue("non-existing-section", "non-existing-key", "non-existing-value"));
    }

    [TestMethod]
    public void TestParserConfigGetIntValue()
    {   
        ParserConfig parser = new ParserConfig(exampleFile);
        Assert.AreEqual(10, parser.GetIntValue("cookies", "yummu_cookie_amount"));
        
    }

    [TestMethod]
    public void TestParserConfigGetIntValueFallback()
    {   
        ParserConfig parser = new ParserConfig(exampleFile);
        Assert.AreEqual(10, parser.GetIntValue("cookies", "cookie_monster", 10));
        
    }

    [TestMethod]
    public void TestParserConfigGetIntValueFallbackNotExists()
    {   
        ParserConfig parser = new ParserConfig(exampleFile);
        Assert.AreEqual(null, parser.GetIntValue("cookies", "cookie_monster"));
        
    }

}