using System.Collections.Generic;
using System;

namespace Parser;


public class ParserConfig
{   
    private string[] filenameContains;
    private List<string> allSections = new List<string>();

    private Dictionary<string, Dictionary<string, string>> nested_dict = new Dictionary<string, Dictionary<string, string>>();

    public ParserConfig(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        this.filenameContains = lines;
        this.allSections = this.GetSections();
        if (this.IsFileValid()){
            this.FillDict();
        }
        
    }

    public List<string> GetSections()
    {
        List<string> all_sections = new List<string>();
        foreach (string line in this.filenameContains)
        {
            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                all_sections.Add(line.Replace("[", "").Replace("]",""));
            }
        }
        return all_sections;
    }

    private bool IsFileValid()
    {
        if (this.allSections.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void FillDict()
    {   
        string current_section = "";
        foreach (string line in this.filenameContains)
        {
            if (line.StartsWith("[") && line.EndsWith("]")){
                current_section  = line.Replace("[", "").Replace("]", "");
                nested_dict.Add(current_section, new Dictionary<string, string>());
            }

            if (!String.IsNullOrEmpty(line) && !line.StartsWith("[") && current_section != "")
            {
                string[] temp_list = line.Replace(" ", "").Split("=");
                string valueName = temp_list[0];
                string valueValue = temp_list[1];
                nested_dict[current_section].Add(valueName, valueValue);
            } 
        }
    }

    private string ReturnNestedValue(string section, string prop_name)
    {
        return nested_dict[section][prop_name];
    }

    public string GetValue(string section, string prop_name, string fallback="")
    {   try
        {
            return this.ReturnNestedValue(section, prop_name);
        }
        catch (System.Collections.Generic.KeyNotFoundException error)
        {
            return fallback;
        }
        
    }

    public int? GetIntValue(string section, string prop_name, int? fallback=null)
    {   try
        {
            string value = this.ReturnNestedValue(section, prop_name);
            return Convert.ToInt32(value);
        }
        catch(System.Collections.Generic.KeyNotFoundException error)
        {
            return fallback;
        }
        
    }
}
