using CoMapAPP.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

//Data processing application
//Develop a console application for processing entrance examination results, which loads data from the report written in text file (see attached examination.txt), processes data and saves results in a suitable XML/JSON form to a file.
//The correct input (rating) is an integer ranging from 0 to 100.
//For each student, the application must count average using the following weights: Math 40 %, Physics 35 %, English 25 %.
//For each subject, the application must count simple average, median and modus for all data records and also separately for each group.
//Resolve corrupted records and write report about them into XML/JSON file.
//Results must be writable into file in XML or JSON format (command line option â€“ XML is default). Develop your own suitable XML/JSON structure.
//Code must be written in C# (Microsoft C# Coding Conventions) and compilable under .NET Core 3.1, preferably .NET 5.
//In design, take into consideration extensibility (perhaps more subjects, bigger >mount of data) and how would you expect your program to be further used/ran by others.
//Deliver the whole developed project in git repository (git archive --format zip --output /full/path/to/zipfile.zip master) or packed zip file (git is preferred option).
//Please also include time spent on solving this task in your response.


namespace CoMapAPP
{
	class Program
	{
		static void Main(string[] args) {
			List<string> errorList = new List<string>();
			string filePath = @".\Resources\examination.txt";
			List<string> averageMedianModusList = AppManager.GetAverageModusMedian(filePath);
			List<Group> groupList = AppManager.GetGroupList(filePath, out errorList);
			if (errorList.Count > 0) {
				foreach (string error in errorList) {
					if (error.Length > 0) {
						Console.WriteLine(error);
					}
				}
			}

			WriteResultsOnConsole(averageMedianModusList, groupList);
			Console.WriteLine("\n\n");
			Console.WriteLine(SerializeResult(groupList));

			Console.WriteLine("To save results into file press S: ");
			if (Console.ReadKey().Key == ConsoleKey.S) {
				Console.WriteLine("\nFile name: ");
				string fileName = Console.ReadLine();
				SaveIntoFile(fileName, groupList);
			}
		}

		static void SaveIntoFile(string fileName, List<Group> groupList) {
			string text = SerializeResult(groupList);
			string path = fileName + ".txt";
			FileStream f = new FileStream(path, FileMode.OpenOrCreate);
			StreamWriter s = new StreamWriter(f);
			s.WriteLine(text);
			s.Close();
			f.Close();
			Console.WriteLine("Successfully saved as " + fileName);
		}

		

		static string SerializeResult(List<Group> groupList) {
			string serializedObject = JsonConvert.SerializeObject(groupList);
			return serializedObject;
		}

		static void WriteResultsOnConsole(List<string> ammList,List<Group> groupList ) {
			
			Console.WriteLine();
			foreach (string row in ammList) {
				Console.WriteLine(row);
			}
			int i = 0;
			foreach (Group grp in groupList) {
				i++;
				Console.WriteLine("\n Group" + i);
				foreach (string str in grp.Average) {
					Console.WriteLine(str);
				}
				Console.WriteLine();
				foreach (string str in grp.Median) {
					Console.WriteLine(str);
				}
				Console.WriteLine();
				foreach (string str in grp.Modus) {
					Console.WriteLine(str);
				}

				foreach (Student std in grp) {
					Console.WriteLine(std.Name);
					foreach (Subject sbj in std.Subjects) {
						Console.WriteLine(sbj.SubjectName + ": " + sbj.Weight);
					}
				}
			}
		}

		
	}
}
