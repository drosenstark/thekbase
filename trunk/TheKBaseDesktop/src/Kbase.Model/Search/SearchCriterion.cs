/*
This file is part of TheKBase Desktop
A Multi-Hierarchical  Information Manager
Copyright (C) 2004-2006 Daniel Rosenstark

TheKBase Desktop is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
("GPL") version 2 as published by the Free Software Foundation.
See the file LICENSE.TXT for the full text of the GNU GPL, or see
http://www.gnu.org/licenses/gpl.txt

For using TheKBase Desktop with software that can not be combined with 
the GNU GPL or any other queries, please contact Daniel Rosenstark 
(license@thekbase.com).
*/
using System;

namespace Kbase.Model.Search
{

	public struct SearchCriterion
	{
		public bool IgnoreCase;
		public SearchTypeIsContains IsContains;
		public string Word;
		public SearchTypeWhere Where;
		public SearchTypeConcat ConcatWithLast;
		public SearchTypeTextTitle TextTitle;
        public bool IsBlank() {
            return (Word == null || Word.Length == 0);
        }

        /*
        public PCriterion GetPCriterion() {
            if (Word == null || Word.Length == 0)
                return null;

            PCriterion retVal = new PCriterion();

            retVal.Word = Word;

            if (Where == SearchTypeWhere.Own)
                retVal.OwnParentParentOrOwn = 0;
            else if (Where == SearchTypeWhere.Parent)
                retVal.OwnParentParentOrOwn = 1;
            else if (Where == SearchTypeWhere.Parent_or_Own)
                retVal.OwnParentParentOrOwn = 2;
            else
                throw new Exception("You cannot search for ancestors in ClientServer");

            if (TextTitle == SearchTypeTextTitle.Text)
                retVal.TextTitleBothIdIcon = 0;
            else if (TextTitle == SearchTypeTextTitle.Title)
                retVal.TextTitleBothIdIcon = 1;
            else if (TextTitle == SearchTypeTextTitle.Text_or_Title)
                retVal.TextTitleBothIdIcon = 2;
            else if (TextTitle == SearchTypeTextTitle.Id)
                retVal.TextTitleBothIdIcon = 3;
            else if (TextTitle == SearchTypeTextTitle.Icon)
                retVal.TextTitleBothIdIcon = 4;
            else
                throw new Exception("texttitleboth etc. has an incorrect value");

            if (ConcatWithLast == SearchTypeConcat.And)
                retVal.AndOrNot = 0;
            else if (ConcatWithLast == SearchTypeConcat.Or)
                retVal.AndOrNot = 1;
            else if (ConcatWithLast == SearchTypeConcat.Not)
                retVal.AndOrNot = 2;

            if (retVal.TextTitleBothIdIcon != 3 && IsContains == SearchTypeIsContains.Is)
                throw new Exception("Only CONTAINS searching is allowed in ClientServer version");

            return retVal;
        }
        */
    }

    public class SearchCriterionConverter {
        public static string GetEnumAsText(object o)
        {
            string retVal = o.ToString();
            return retVal.Replace("_", " ");
        }

        public static object GetTextAsEnum(string text, Type enumType)
        {
            text = text.Replace(" ", "_");
            return Enum.Parse(enumType, text);
        }    
    }

	public enum SearchTypeTextTitle
	{
		Title = 1,
		Text = 2,
		Text_or_Title = Title | Text,
        Id = 4,
        Icon = 8
	}


	public enum SearchTypeIsContains
	{
		Contains,
		Is
	}

	public enum SearchTypeWhere
	{
		Own = 1,
		Parent = 2,
		Ancestor = 4,
        Ancestor_or_Own = Ancestor | Own,
        Parent_or_Own = Parent | Own
	}

	public enum SearchTypeConcat
	{
		And,
		Or,
		Not,
		None
	}
}
