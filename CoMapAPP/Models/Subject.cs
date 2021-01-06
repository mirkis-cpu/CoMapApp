using System;
using System.Collections.Generic;
using System.Text;

namespace CoMapAPP.Models
{
	public class Subject
	{
		public string SubjectName { get; set; }
		public int Weight { get; set; }

		public Subject(string subjectName, int weight) {
			SubjectName = subjectName;
			Weight = weight;
		}
	}
}
