using System.Collections.Generic;
using System.Linq;

namespace CoMapAPP.Models
{

	//For each subject, the application must count simple average, median and modus for all data records and also separately for each group.s
	public class Group : List<Student>
	{
		public List<string> Average {
			get {
				return ReturnAverage();
			}
			private set { }
		}

		public List<string> Median {
			get {
				return ReturnMedian();
			}
			private set { }
		}
		public List<string> Modus {
			get {
				return ReturnModus();
			}
			private set { }
		}

		private List<string> ReturnAverage() {
			Dictionary<string, double> averageList = new Dictionary<string, double>();
			foreach (Student std in this) {
				foreach (Subject sbj in std.Subjects) {
					if (averageList.ContainsKey(sbj.SubjectName)){
						averageList[sbj.SubjectName] += sbj.Weight;
					} else {
						averageList.Add(sbj.SubjectName, 0);
					}
					
				}
			}
			foreach (Subject sbj in this.Where(obj => obj.Subjects.Count > 0).First().Subjects) {
				averageList[sbj.SubjectName] = averageList[sbj.SubjectName] / this.Count;
			}
			List<string> output = new List<string>();
			foreach(var item in averageList) {
				output.Add(string.Format("{0} average : {1}", item.Key, item.Value));
			}
			return output;
		}

		private List<string> ReturnMedian() {
			Dictionary<string, decimal> medianList = new Dictionary<string, decimal>();
			Dictionary<string, List<int>> medianHelpList = new Dictionary<string, List<int>>();
			foreach (var sbj in this[0].Subjects) {
				medianList.Add(sbj.SubjectName, 0);
				medianHelpList.Add(sbj.SubjectName, new List<int>());
			}

			foreach (Student std in this) {
				foreach (Subject sbj in std.Subjects) {
					if (!medianList.ContainsKey(sbj.SubjectName)) {
						medianList.Add(sbj.SubjectName, 0);
					}
					if (!medianHelpList.ContainsKey(sbj.SubjectName)) {
						medianHelpList.Add(sbj.SubjectName, new List<int>());
					} else {
						medianHelpList[sbj.SubjectName].Add(sbj.Weight);
					}
				}
			}

			foreach (Subject sbj in this.Where(obj => obj.Subjects.Count > 0).First().Subjects) {
				medianList[sbj.SubjectName] = AppManager.GetMedian(medianHelpList[sbj.SubjectName]);
			}

			List<string> output = new List<string>();
			foreach (var item in medianList) {
				output.Add(string.Format("{0} median : {1}", item.Key, item.Value));
			}
			return output;
		}

		private List<string> ReturnModus() {
			Dictionary<string, List<int>> modusList = new Dictionary<string, List<int>>();
			Dictionary<string, List<int>> modusHelpList = new Dictionary<string, List<int>>();
			foreach (Subject sbj in this[0].Subjects) {
				
				
				modusHelpList.Add(sbj.SubjectName, new List<int>());
			}

			foreach (Student std in this) {
				foreach (Subject sbj in std.Subjects) {
					if(!modusList.ContainsKey(sbj.SubjectName)) {
						modusList.Add(sbj.SubjectName, new List<int>());
					}
					if(!modusHelpList.ContainsKey(sbj.SubjectName)) {
						modusHelpList.Add(sbj.SubjectName, new List<int>());
					} else {
						modusHelpList[sbj.SubjectName].Add(sbj.Weight);
					}
				}
			}

			foreach (Subject sbj in this.Where(obj=>obj.Subjects.Count>0).First().Subjects) {
				modusList[sbj.SubjectName] = AppManager.GetModus(modusHelpList[sbj.SubjectName]);
			}
			List<string> output = new List<string>();
			foreach (var item in modusList) {
				string modes = string.Empty;
				foreach(int mod in item.Value) {
					modes += mod + " ";
				}
				output.Add(string.Format("{0} modus : {1}", item.Key, modes));
			}
			return output;
		}


	}
}
