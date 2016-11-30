using Spillway.Interfaces;
using System;

namespace Spillway.ViewModels
{
    public class OptionsViewModel : ViewModelBase, ISection
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