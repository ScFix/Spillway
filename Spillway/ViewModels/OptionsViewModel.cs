using Spillway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillway.ViewModels
{
	public class OptionsViewModel: ViewModelBase, ISection
	{
		public override string ToString()
		{
			return "Options";
		}

		public String SectionName
		{
			get { return ToString(); }
		}
	}
}