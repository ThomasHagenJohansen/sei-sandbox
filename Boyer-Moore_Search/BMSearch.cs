using System;
using System.Collections;

namespace Boyer_Moore_Search
{
	// This class implementes a tuned Boyer - Moore search algorithm
	// inspired from this homepage:
	// 
	// http://www-igm.univ-mlv.fr/~lecroq/string/node1.html

	#region BMPattern class
	/// <summary>
	/// This class holds information about a single search pattern
	/// </summary>
	public class BMPattern
	{
		/********************************************************************/
		/// <summary>
		/// Constructor
		/// </summary>
		/********************************************************************/
		public BMPattern(String pattern)
		{
			_Pattern = pattern.ToUpper();

			// Start to find the heighest character, so we know how big
			// our table should be
			int maxChar = 0;
			foreach (char chr in _Pattern)
				maxChar = Math.Max(maxChar, (int)chr);

			// Now create the shift table
			int patternLen = _Pattern.Length;
			_ShiftTable    = new int[maxChar + 1];

			for (int i = 0; i <= maxChar; i++)
				_ShiftTable[i] = patternLen;

			for (int i = 0; i < patternLen - 1; i++)
				_ShiftTable[(int)_Pattern[i]] = patternLen - i - 1;

			// Prepare some other values
			int lastCharVal          = (int)_Pattern[patternLen - 1];
			_ShiftValue              = _ShiftTable[lastCharVal];
			_ShiftTable[lastCharVal] = 0;
		}



		/********************************************************************/
		/// <summary>
		/// Return the pattern string
		/// </summary>
		/********************************************************************/
		public String Pattern
		{
			get
			{
				return _Pattern;
			}
		}
		private String _Pattern;



		/********************************************************************/
		/// <summary>
		/// Return the shift table
		/// </summary>
		/********************************************************************/
		internal int[] ShiftTable
		{
			get
			{
				return _ShiftTable;
			}
		}
		private int[] _ShiftTable;



		/********************************************************************/
		/// <summary>
		/// Return the shift value
		/// </summary>
		/********************************************************************/
		internal int ShiftValue
		{
			get
			{
				return _ShiftValue;
			}
		}
		private int _ShiftValue;
	}
	#endregion

	#region BMSearch class
	/// <summary>
	/// The main search class
	/// </summary>
	public class BMSearch
	{
		private String originalMainText;
		private String modifiedMainText;

		private BMPattern searchPattern;
		private int searchIndex;

		/********************************************************************/
		/// <summary>
		/// Constructor
		/// </summary>
		/********************************************************************/
		public BMSearch(String text)
		{
			Initialize(text, null);
		}



		/********************************************************************/
		/// <summary>
		/// Constructor
		/// </summary>
		/********************************************************************/
		public BMSearch(String text, String pattern)
		{
			Initialize(text, new BMPattern(pattern));
		}



		/********************************************************************/
		/// <summary>
		/// Constructor
		/// </summary>
		/********************************************************************/
		public BMSearch(String text, BMPattern pattern)
		{
			Initialize(text, pattern);
		}



		/********************************************************************/
		/// <summary>
		/// Sets the pattern string to search for
		/// </summary>
		/********************************************************************/
		public void SetPattern(String pattern)
		{
			SetPattern(new BMPattern(pattern));
		}



		/********************************************************************/
		/// <summary>
		/// Sets the pattern string to search for
		/// </summary>
		/********************************************************************/
		public void SetPattern(BMPattern pattern)
		{
			searchPattern = pattern;

			String patternText = pattern.Pattern;
			modifiedMainText   = (originalMainText + new String(patternText[patternText.Length - 1], patternText.Length)).ToUpper();

			Reset();
		}



		/********************************************************************/
		/// <summary>
		/// Resets the search to start over again
		/// </summary>
		/********************************************************************/
		public void Reset()
		{
			searchIndex = 0;
		}



		/********************************************************************/
		/// <summary>
		/// Will search for the next instance and return the index
		/// in the main text where it found the search string.
		/// Returns -1 if no more strings are found.
		/// </summary>
		/********************************************************************/
		public int FindNext()
		{
			int charNum, skipNum;

			if ((searchPattern == null) || (searchIndex == -1))
				return -1;

			String patternText = searchPattern.Pattern;
			int patternTextLen = patternText.Length;

			int mainTextLen  = originalMainText.Length;
			int shiftValue   = searchPattern.ShiftValue;
			int[] shiftTable = searchPattern.ShiftTable;
			int shiftLen     = shiftTable.Length;

			while (searchIndex < (mainTextLen - patternTextLen))
			{
				charNum = (int)modifiedMainText[searchIndex + patternTextLen - 1];
				if (charNum > shiftLen)
					skipNum = patternTextLen;
				else
					skipNum = shiftTable[charNum];

				while (skipNum != 0)
				{
					searchIndex += skipNum;
					charNum      = (int)modifiedMainText[searchIndex + patternTextLen - 1];
					if (charNum > shiftLen)
						skipNum = patternTextLen;
					else
						skipNum = shiftTable[charNum];

					searchIndex += skipNum;
					charNum      = (int)modifiedMainText[searchIndex + patternTextLen - 1];
					if (charNum > shiftLen)
						skipNum = patternTextLen;
					else
						skipNum = shiftTable[charNum];

					searchIndex += skipNum;
					charNum      = (int)modifiedMainText[searchIndex + patternTextLen - 1];
					if (charNum > shiftLen)
						skipNum = patternTextLen;
					else
						skipNum = shiftTable[charNum];
				}

				if (String.Compare(patternText, 0, modifiedMainText, searchIndex, patternTextLen - 1, false) == 0)
				{
					int retVal   = searchIndex;
					searchIndex += shiftValue;
					return retVal;
				}

				searchIndex += shiftValue;
			}

			return -1;
		}



		/********************************************************************/
		/// <summary>
		/// Initializes the class
		/// </summary>
		/********************************************************************/
		private void Initialize(String text, BMPattern pattern)
		{
			originalMainText = text;
			SetPattern(pattern);
		}
	}
	#endregion
}
