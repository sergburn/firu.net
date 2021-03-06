﻿using System.ComponentModel;
using System.Diagnostics;

namespace FiruModel
{
    public enum MarkValue
    {
        Unknown = 0,
        ToLearn = 1,
        WithHints = 2,
        AlmostLearned = 3,
        Learned = 4
    }

    public class Mark
    {
        public static MarkValue Upgrade(MarkValue current)
        {
            if (current < MarkValue.Learned)
            {
                return (MarkValue)((int)current + 1);
            }
            return current;
        }

        public static MarkValue Downgrade(MarkValue current)
        {
            if (current > MarkValue.ToLearn)
            {
                return (MarkValue)((int)current - 1);
            }
            return current;
        }

        public static MarkValue UpdateToTestResult(MarkValue current, TestResult result)
        {
            MarkValue updated = current;
            switch (result)
            {
                case TestResult.Passed:
                    if (current == MarkValue.ToLearn)
                        updated = MarkValue.AlmostLearned;
                    else
                        updated = Upgrade(current);
                    break;

                case TestResult.PassedWithHints:
                    updated = MarkValue.WithHints;
                    break;

                case TestResult.Failed:
                    updated = MarkValue.ToLearn;
                    break;

                default:
                    Debug.WriteLine("Unexpected test result {0} in Mark::updateToTestResult", result);
                    break;
            }
            Debug.WriteLine("Mark changed from {0} to {1}", current, updated);
            return updated;
        }

        public static string ToString(MarkValue mark)
        {
            switch (mark)
            {
                case MarkValue.ToLearn:
                    return "learning";
                case MarkValue.WithHints:
                    return "hints required";
                case MarkValue.AlmostLearned:
                    return "well known";
                case MarkValue.Learned:
                    return "learned";
                default:
                    return "<none>";
            }
        }
    }
}
