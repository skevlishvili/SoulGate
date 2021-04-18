using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HelpMethods
{
    public static string RandomString(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var stringChars = new char[length];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);


        return finalString;
    }

    public static int GetSkillIndexByName(String Skillname){
        int _SkillIndex = 0;

        for (int i = 1; i<SkillLibrary.Skills.Length; i++)
        {
            if (Skillname == SkillLibrary.Skills[i].SkillName)
            {
                _SkillIndex = i;
            }
        }

        return _SkillIndex;
    }
}
