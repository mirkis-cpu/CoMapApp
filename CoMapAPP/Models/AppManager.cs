using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoMapAPP.Models
{
	public static class AppManager
	{

		private static List<string> GetLinesFromFile(string filePath) {
			List<string> lines = new List<string>();

			using (StreamReader sr = new StreamReader(filePath)) {
				string str = string.Empty;

				while (!str.StartsWith("Group")) {
					str = sr.ReadLine();
				}

				if (str.StartsWith("Group")) {
					while (sr.Peek() >= 0) {
						if (str != "") {
							lines.Add(str);
							str = sr.ReadLine();
						} else {
							str = sr.ReadLine();
						}
					}
					lines.Add(str);
				}
			}
			return lines;
		}

		public static List<string> GetAverageModusMedian(string filePath) {
			List<string> errorList = new List<string>();
			List<string> outputList = new List<string>();
			Group group = GetGroupList(filePath,out errorList, false).First();
			outputList.AddRange(group.Average);
			outputList.AddRange(group.Median);
			outputList.AddRange(group.Modus);
			return outputList;
		}

		public static List<Group> GetGroupList(string filePath, out List<string> errorList,bool moreGroups = true) {
			errorList = new List<string>();
			string error;
			List<Group> groupList = new List<Group>();
			List<string> lines = GetLinesFromFile(filePath);
			Group group = new Group();
			(string, List<Subject>) studentInput;
			foreach (string line in lines) {
				if (line.StartsWith("Group") && moreGroups) {
					if (group.Count > 0) {
						groupList.Add(group);
					}
					group = new Group();
				} else {
					studentInput = GetStudentInputFromString(line, out error);
					errorList.Add(error);
					group.Add(new Student(studentInput.Item1, studentInput.Item2));
				}
			}
			groupList.Add(group);
			return groupList;
		}

		private static (string, List<Subject>) GetStudentInputFromString(string line,out string error) {
			string Name;
			error = string.Empty;
			List<Subject> _subjects = new List<Subject>();
			string[] splitLines = line.Split(';');
			Name = splitLines[0];
			for (int i = 1; i < splitLines.Length; i++) {
				string[] subjectInfo = splitLines[i].Split('=');
				int weight = int.Parse(subjectInfo[1]);
				if (0 > weight || weight > 100) {
					error = string.Format(Name+": weight of "+ subjectInfo[0] +" is not in limit");
				}
				_subjects.Add(new Subject(subjectInfo[0], weight));

			}
			return (Name, _subjects);
		}

		

		public static decimal GetMedian(List<int> xs) {
			var ys = xs.OrderBy(x => x).ToList();
			double mid = (ys.Count - 1) / 2.0;
			return (ys[(int)(mid)] + ys[(int)(mid + 0.5)]) / 2;
		}

		public static List<int> GetModus(List<int> xs) {
			Dictionary<int, int> modus = new Dictionary<int, int>();
			List<int> modusResult = new List<int>();
			foreach (int cislo in xs) {
				if (!modus.ContainsKey(cislo)) {
					modus.Add(cislo, 1);
				} else {
					modus[cislo]++;
				}
			}
			int max = modus.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
			while (modus.Aggregate((l, r) => l.Value > r.Value ? l : r).Key == max) {
				modusResult.Add(max);
				modus.Remove(modus.Aggregate((l, r) => l.Value > r.Value ? l : r).Key);
			}
			return modusResult;

		}
	}
}



