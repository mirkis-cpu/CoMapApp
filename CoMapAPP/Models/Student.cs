
using System.Collections.Generic;

namespace CoMapAPP.Models
{
	public class Student
	{
		public string Name { get; set; }
		public List<Subject> Subjects { get; set; }		

		public Student(string name, List<Subject> subjects) {
			Name = name;
			Subjects = subjects;
		}


	}
}
