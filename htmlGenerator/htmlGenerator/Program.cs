/*
 * Created by SharpDevelop.
 * User: pengwa
 * Date: 2015/6/22
 * Time: 11:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace htmlGenerator
{	
	class Program
	{
		//Office 2013 App,Office 扩展应用, Office 插件开发, 跨Office 平台应用, simpeng
		private const string globalKeywords = @"Office 2013 App,Office 扩展应用, Office 插件开发, 跨Office 平台应用, 
									simpeng, simpeng.net, Office Add-In, App for Office";
		private const string navarBand = "开发 Office 平台上的扩展应用";
		private const string pageTitlePostfix = "Office 扩展应用文档";
		
		public static void Main(string[] args)
		{
			string template = File.ReadAllText("template.html");
			int endIndex = 0;
			string navigationMenu = GenerateNavigationMenu("chapter1.xml", 1, out endIndex);
			int endIndex2 = 0;
			navigationMenu += GenerateNavigationMenu("chapter2.xml", endIndex, out endIndex2);
			navigationMenu += GenerateNavigationMenu("chapter3.xml", endIndex2, out endIndex);
			
			int endIndex3 = GenerateHtmlPages("chapter1.xml", template, navigationMenu, 1);
			int endIndex4 = GenerateHtmlPages("chapter2.xml", template, navigationMenu, endIndex3);
			endIndex4 = GenerateHtmlPages("chapter3.xml", template, navigationMenu, endIndex4);
		}
		
		private static string GenerateNavigationMenu(string docName, int startIndex, out int endIndex)
		{
			XDocument doc = XDocument.Load( docName );
            IEnumerable<XElement> chapters = doc.Descendants( "Chapter" );

            string chapterIdPrefix= "oai-chapter-";
            StringBuilder navStr = new StringBuilder();
            bool isFirstClickableSectionNode = true;
            int chapterIndex = startIndex;
            foreach ( var chapter in chapters)
            {  	
            	string pageTitle = chapter.Element("Title").Value;
            	string chapterId = chapterIdPrefix + chapterIndex;

            	navStr.AppendLine("<li id=\""+ chapterId +"\" class=\"active oai-chapter-row\"><a>" + pageTitle + "</a></li>");
            	var sections = chapter.Descendants( "Section" );
            	var sectionIndex = 1;
            	string sectionClass = chapterId + "-section";
            	foreach ( var section in sections )
            	{
            		string sectionId = chapterId + "-section-" + sectionIndex;
            		string sectionName = section.Element("Name").Value;
            		string sectionTitle = section.Element("Title").Value;
            		string url = "../" + chapterId + "/" + sectionName + ".html?s=" + sectionId;
            		
            		string extraClass = "";
            		if(isFirstClickableSectionNode == true)
            		{
            			extraClass = "activedSectionRow";
            			isFirstClickableSectionNode = false;
            		}
            		
            		navStr.AppendLine("<li id=\""+ sectionId +"\" class=\"" + sectionClass + " oai-chapter-section-row " + extraClass + " \" style=\"display:none\"><a href=\"" + url + "\">" + sectionTitle + "</a></li>");
            		var subSections = section.Descendants( "SubSection" );
            		
            		sectionIndex++;
            	}
            	chapterIndex++;
            }
            endIndex = chapterIndex;
            return navStr.ToString();
		}
		
		private static int GenerateHtmlPages(string docName, string template, string navigationMenu, int startIndex = 1)
		{
			XDocument doc = XDocument.Load( docName );
            IEnumerable<XElement> chapters = doc.Descendants( "Chapter" );
			string fileTemplateWithNav = "";
            string chapterIdPrefix= "oai-chapter-";
           
            fileTemplateWithNav = template.Replace( "SIMPENG-NET-OAI-NAVIGATION-MENU", navigationMenu);
            fileTemplateWithNav = fileTemplateWithNav.Replace("SIMPENG-NET-OAI-PAGE-NAVBAR-BAND", navarBand);
            fileTemplateWithNav = fileTemplateWithNav.Replace("SIMPENG-NET-OAI-DOCUMENT-KEYWORDS", globalKeywords);    
            fileTemplateWithNav = fileTemplateWithNav.Replace("SIMPENG-NET-OAI-DOCUMENT-DESCRIPTION", globalKeywords);            
            int chapterIndex = startIndex;
            foreach ( var chapter in chapters )
            {
            	string chapterTitle = chapter.Element("Title").Value;
            	string chapterName = chapter.Element("Name").Value;
            	var sections = chapter.Descendants( "Section" );
            	string chapterId = chapterIdPrefix + chapterIndex;
            	
            	if(!Directory.Exists(chapterId))
            	{
            		Directory.CreateDirectory(chapterId);	
            	}

            	foreach ( var section in sections )
            	{
            	    // For each document page, we start to handle 
            		string sectionName = section.Element("Name").Value;
            		string sectionTitle = section.Element("Title").Value;
            		string location = chapterTitle + "》" + sectionName;
            		
            		string pageContent = fileTemplateWithNav.Replace("SIMPENG-NET-OAI-PAGE-TITLE", sectionTitle + " - " + pageTitlePostfix );
            		pageContent = pageContent.Replace("SIMPENG-NET-OAI-PAGE-CURRENTLOCATION", chapterTitle + "》" + sectionTitle);
            		pageContent = pageContent.Replace("SIMPENG-NET-OAI-PAGE-HEADER", sectionTitle);
            		
            		var subSections = section.Descendants( "SubSection");
            		StringBuilder documentContent = new StringBuilder();
            		foreach(var subSection in subSections)
            		{
            			documentContent.AppendLine("<h3 class=\"sub-header\">" + subSection.Element("Title").Value + "</h3>");
            			
            			var paragraphs = subSection.Descendants("Paragraph");
            			foreach( var p in paragraphs)
            			{
            				documentContent.AppendLine("<p>" + p.Value + "</p>");
            			}
            		}
            		pageContent = pageContent.Replace("SIMPENG-NET-OAI-PAGE-DOCUMENTCONTENT", documentContent.ToString());
            		
            		File.WriteAllText( GetHtmlPageRelativePath(chapterId, sectionName), pageContent);
				}
            	chapterIndex++;
            }
            
            return chapterIndex;
		}
		
		private static string GetHtmlPageRelativePath(string chapterId, string sectionName)
		{
			return chapterId + "\\" + sectionName + ".html";
		}
	}
}