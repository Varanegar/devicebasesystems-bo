using System;
using System.Threading;

namespace Anatoli.SDS.DataAccess.Helpers
{
    public class TimeValueException:Exception
    {
        public TimeValueException(string message):base(message)
        {

        }
    }

	public class PersianDate:IComparable
	{
		private const int era = 0;
		private const int monthsInYear = 12;
		private const long ticksPerDay = 864000000000L;
		private const int daysIn33Years = 365 * 33 + 8;
		private static int[] leapFlag = {0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0};
		private DateTime m_DateTime;

		private static double Solar = 365.25;
		private static int GYearOff = 226894;
		private static int[,] GDayTable = new int[,] { { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }, { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 } };
		private static int[,] JDayTable = new int[,] { { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29 }, { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 30 } };
		private static string[] weekdays = new string[] { "í˜ÔäÈå", "ÏæÔäÈå", "Óå ÔäÈå", "åÇÑÔäÈå", "äÌÔäÈå", "ÌãÚå" ,"ÔäÈå"};
		private static string[] weekdaysabbr = new string[] { "í", "Ï", "Ó", "", "", "Ì" ,"Ô"};

        public static string[] MonthNames = { "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
        public static string[] AbrMonthNames = { "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند" };
		public static string[] DayNames = {"íßÔäÈå", "ÏæÔäÈå", "Óå ÔäÈå", "åÇÑÔäÈå", "äÌ ÔäÈå", "ÌãÚå","ÔäÈå"};
		public static string[] AbrDayNames = {"ش", "ي", "د", "س", "چ", "پ", "ج"};
		public static string NumberDecimalSeparator=".";
		public static string CurrencyDecimalSeparator=".";
		public static string ShortDatePattern="yymmdd";
		public static string LongDatePattern="yyyy/mm/dd";
		public static string LongTimePattern="hh:mm:ss";
		public static string NegativeSign="-";
		public static string TimeSeparator=":";
		public static string AMDesignator="Þ.Ö.";
		public static string PMDesignator="È.Ö.";
		public static string YearMonthPattern="MMMM yyyy";
		public static string MonthDayPattern="mmdd";

        public static PersianDate GetFirstDayOfYear()
        {
            PersianDate obj = PersianDate.Now;
            return new PersianDate(obj.Year, 1, 1);
        }

        public static PersianDate GetLastDayOfYear()
        {
            PersianDate obj = PersianDate.Now;
            return new PersianDate(obj.Year, 12, PersianDate.DaysInMonth(obj.Year, 12));
        }

        public static PersianDate GetFirstDayOfMonth()
        {
            PersianDate obj = PersianDate.Now;
            return new PersianDate(obj.Year, obj.Month, 1);
        }

        public static PersianDate GetLastDayOfMonth()
        {
            PersianDate obj = PersianDate.Now;
            return new PersianDate(obj.Year, obj.Month, PersianDate.DaysInMonth(obj.Year, obj.Month));
        }

        public static bool IsDateValid(string date)
        {
            try
            {
                if (date == null) return true;

                string[] s = date.Replace(" ","/").Replace(":","/").Split('/');
                if (s.Length < 3) return false;
                if (s[0].Trim() == "") return false;
                if (s[0].Length != 4) return false;
                if (s[1].Trim() == "") return false;
                if (s[1].Length != 2) return false;
                if (s[2].Trim() == "") return false;
                if (s[2].Length != 2) return false;
                if (Convert.ToInt32(s[0]) < 1301) return false;
                if (Convert.ToInt32(s[1]) > 12) return false;
                if (Convert.ToInt32(s[1]) < 1) return false;
                if (Convert.ToInt32(s[2]) > PersianDate.DaysInMonth(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]))) return false;
                if (Convert.ToInt32(s[2]) < 1) return false;

                if (s.Length == 4) return false;
                if (s.Length > 5) return false;

                if (s.Length > 3)
                {
                    if (Convert.ToInt32(s[3]) > 23) return false;
                    if (Convert.ToInt32(s[4]) > 59) return false;
                }
            }
            catch 
            {
                return false;
            }
            
            return true;
        }

        public static PersianDate GetFirstDayOfWeek()
        {
            PersianDate obj = PersianDate.Now;
            obj = obj.AddDays(-1 * obj.GetDayOfWeek());

            return obj;
        }

        public static PersianDate GetLastDayOfWeek()
        {
            PersianDate obj = PersianDate.Now;
            obj = obj.AddDays(6 - obj.GetDayOfWeek());
            return obj;
        }
        
        public static System.Globalization.CalendarWeekRule CalendarWeekRule
		{
			get { return System.Globalization.CalendarWeekRule.FirstFourDayWeek;}
		}

		public static DayOfWeek FirstDayOfWeek
		{
			get 
            {
                if (true)
                    return DayOfWeek.Saturday;
                else
                    return DayOfWeek.Sunday;
            }
		}

		public static int ToFourDigitYear(int year )
		{
            int baseyear = 0;
            if (true)
                baseyear = 1300;
            else
                baseyear = 2000;


			if (year<100) 
                return baseyear+year;
			else 
                return year;
		}

		public static string GetAbbreviatedMonthName(int month)
		{
            if (true)
                return AbrMonthNames[month];
            else
                return Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[month];
		}

		public static string GetMonthName(int month)
		{
            if (true)
                return MonthNames[month];
            else
                return Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames[month];
        }

		public static string GetDayName(DayOfWeek day)
		{
            if (true)
                return DayNames[(int)day];
            else
                return Thread.CurrentThread.CurrentCulture.DateTimeFormat.DayNames[(int)day];
		}

		public static string GetAbbreviatedDayName(DayOfWeek dayOfWeek )
		{
            if (true)
                return AbrDayNames[(int)dayOfWeek];
            else
                return Thread.CurrentThread.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[(int)dayOfWeek];
		}

		public System.DateTime DateTime
		{
			get { return m_DateTime; }
		}

		public static PersianDate MaxValue
		{
			get {return new PersianDate(DateTime.MaxValue);}
		}

		public static PersianDate MinValue
		{
			get {return new PersianDate(DateTime.MinValue);}
		}

		public static PersianDate Parse(string value)
		{
			return new PersianDate(StringToDateTime(value));
		}

		public static PersianDate Parse (string s , System.IFormatProvider provider )
		{
			return new PersianDate(StringToDateTime(s));
		}

		public static PersianDate Parse(string s ,System.IFormatProvider provider , System.Globalization.DateTimeStyles styles )
		{
			return new PersianDate(StringToDateTime(s));
		}

		public static DateTime StringToDateTime(string value)
		{
			string[] dt = value.Split(" ".ToCharArray());

			string[] dateParts = dt[0].Split("/".ToCharArray());
			int day = int.Parse(dateParts[2]);
			int month = int.Parse(dateParts[1]);
			int year = int.Parse(dateParts[0]);

            if (year < 1000) year += 1300;

            DateTime datetime;

            if (true)
                datetime = GetGregorianDate(year, month, day);
            else
                datetime = new DateTime(year, month, day);

            if (dt.Length > 1)
            {
                string[] timeParts = dt[1].Split(":".ToCharArray());

                foreach (string s in timeParts)
                {
                    if (s.Trim() == "")
                        throw new TimeValueException("ساعت وارد شده معتبر نیست");
                }

                datetime = datetime.AddHours(int.Parse(timeParts[0]));
                datetime = datetime.AddMinutes(int.Parse(timeParts[1]));
            }

            return datetime;
		}

		public PersianDate()
		{
            m_DateTime = DateTime.Now;
		} 

		public PersianDate(int year , int month , int day )
		{
			m_DateTime=GetGregorianDate(year,month,day);
		}

		public PersianDate(int year , int month , int day , int hour , int minute , int second )
		{
			m_DateTime=GetGregorianDate(year,month,day);
			m_DateTime=m_DateTime.AddHours(hour);
			m_DateTime=m_DateTime.AddMinutes(minute);
			m_DateTime=m_DateTime.AddSeconds(second);
		}

		public PersianDate(int year , int month , int day , int hour , int minute , int second ,int millisecond)
		{
			m_DateTime=GetGregorianDate(year,month,day);
			m_DateTime=m_DateTime.AddHours(hour);
			m_DateTime=m_DateTime.AddMinutes(minute);
			m_DateTime=m_DateTime.AddSeconds(second);
			m_DateTime=m_DateTime.AddMilliseconds(millisecond);
		}

		public PersianDate(DateTime datetime)
		{
			m_DateTime=datetime;
		}

		public int CompareTo(object obj) 
		{
			if(obj is PersianDate) 
			{
				PersianDate temp = (PersianDate) obj;

				return m_DateTime.CompareTo(temp.DateTime);
			}
			throw new ArgumentException("object is not a Temperature");    
		}

		public static int Compare(PersianDate t1 ,PersianDate t2 )
		{
			return DateTime.Compare(t1.DateTime,t2.DateTime);
		}

		public static bool operator <(PersianDate c1, PersianDate c2) 
		{
			try
			{
				DateTime d=c1.DateTime;
			}
			catch
			{
				c1=new PersianDate(DateTime.MinValue);
			}

			try
			{
				DateTime d=c2.DateTime;
			}
			catch
			{
				c2=new PersianDate(DateTime.MinValue);
			}
			return c1.DateTime<c2.DateTime;
		}

		public static bool operator >(PersianDate c1, PersianDate c2) 
		{
			try
			{
				DateTime d=c1.DateTime;
			}
			catch
			{
				c1=new PersianDate(DateTime.MinValue);
			}

			try
			{
				DateTime d=c2.DateTime;
			}
			catch
			{
				c2=new PersianDate(DateTime.MinValue);
			}
			return c1.DateTime>c2.DateTime;
		}

		public static bool operator ==(PersianDate c1, PersianDate c2) 
		{
			/*if (c1 is null) c1=new PersianDate(DateTime.MinValue);
			if (c2==null) c2=new PersianDate(DateTime.MinValue);*/
			try
			{
				DateTime d=c1.DateTime;
			}
			catch
			{
				c1=new PersianDate(DateTime.MinValue);
			}

			try
			{
				DateTime d=c2.DateTime;
			}
			catch
			{
				c2=new PersianDate(DateTime.MinValue);
			}

			return c1.DateTime==c2.DateTime;

		}

		public static bool operator !=(PersianDate c1, PersianDate c2) 
		{
			try
			{
				DateTime d=c1.DateTime;
			}
			catch
			{
				c1=new PersianDate(DateTime.MinValue);
			}

			try
			{
				DateTime d=c2.DateTime;
			}
			catch
			{
				c2=new PersianDate(DateTime.MinValue);
			}
			return c1.DateTime!=c2.DateTime;
		}

		public static bool IsLeapYear(int year)
		{
            if (true)
                return leapFlag[year % 33] != 0;
            else
                return DateTime.IsLeapYear(year);
		}

		public static int DaysInMonth(int year, int month)
		{
            if (true)
            {
                if (month < 7)
                    return 31;
                if (month < 12)
                    return 30;
                if (IsLeapYear(year))
                    return 30;
                return 29;
            }
            else
            {
                return DateTime.DaysInMonth(year, month);
            }
		}

		public static bool operator <=(PersianDate c1, PersianDate c2) 
		{
			try
			{
				DateTime d=c1.DateTime;
			}
			catch
			{
				c1=new PersianDate(DateTime.MinValue);
			}

			try
			{
				DateTime d=c2.DateTime;
			}
			catch
			{
				c2=new PersianDate(DateTime.MinValue);
			}
			return c1.DateTime<=c2.DateTime;
		}

		public static bool operator >=(PersianDate c1, PersianDate c2) 
		{
			try
			{
				DateTime d=c1.DateTime;
			}
			catch
			{
				c1=new PersianDate(DateTime.MinValue);
			}

			try
			{
				DateTime d=c2.DateTime;
			}
			catch
			{
				c2=new PersianDate(DateTime.MinValue);
			}
			return c1.DateTime>=c2.DateTime;
		}

		public static TimeSpan operator -(PersianDate c1, PersianDate c2) 
		{
			try
			{
				DateTime d=c1.DateTime;
			}
			catch
			{
				c1=new PersianDate(DateTime.MinValue);
			}

			try
			{
				DateTime d=c2.DateTime;
			}
			catch
			{
				c2=new PersianDate(DateTime.MinValue);
			}
			return c1.DateTime-c2.DateTime;
		}

		public static PersianDate operator +(PersianDate c1, TimeSpan c2) 
		{
			try
			{
				DateTime d=c1.DateTime;
			}
			catch
			{
				c1=new PersianDate(DateTime.MinValue);
			}

			
			return new PersianDate(c1.DateTime+c2);
		}

        public static PersianDate Now
        {
            get { return new PersianDate(DateTime.Now); }
        }

		public string ToString(String format)
		{
            if (true)
            {
                if (format == YearMonthPattern)
                {
                    int year, month, day;
                    GetJalaliDate(m_DateTime, out year, out month, out day);
                    return MonthNames[month - 1] + " " + Convert.ToString(year);
                }
                if (format == "dd")
                {
                    return DayNames[(GetDayOfYear(m_DateTime) % 7)];
                }
                if (format == "h ")
                {
                    return Convert.ToString(m_DateTime.Hour) + " ";
                }
                if (format == "d")
                {
                    return ToString(true, false);
                }
                return "a";
            }
            else
            {
                return m_DateTime.ToString(format);
            }
		}

        public override string ToString()
        {
            return this.ToString(true, false);
        }

        public string ToDateTimeString()
        {
            return this.ToString(true, true);
        }

        private string ToString(bool retdate, bool rettime)
        {
            if (true)
            {
                int year, month, day;

                GetJalaliDate(m_DateTime, out year, out month, out day);

                string retval = "";

                if (retdate)
                {
                    retval = Convert.ToString(year);

                    if (month < 10)
                        retval += "/0" + month.ToString();
                    else
                        retval += "/" + month.ToString();

                    if (day < 10)
                        retval += "/0" + day.ToString();
                    else
                        retval += "/" + day.ToString();
                }

                if (rettime)
                {
                    if (m_DateTime.Hour > 0 || m_DateTime.Minute > 0)
                    {
                        if (m_DateTime.Hour < 10)
                            retval += " 0" + m_DateTime.Hour.ToString();
                        else
                            retval += " " + m_DateTime.Hour.ToString();

                        if (m_DateTime.Minute < 10)
                            retval += ":0" + m_DateTime.Minute.ToString();
                        else
                            retval += ":" + m_DateTime.Minute.ToString();

                    }
                }

                return retval;
            }
            else
            {
                string format = "";
                if (retdate)
                {
                    format = "yyyy/MM/dd";
                }

                if (retdate && rettime)
                    format += " ";

                if (rettime)
                    format += "HH:mm";

                return m_DateTime.ToString(format);
            }
        }

		public string ToString(string format , System.IFormatProvider provider )
		{
            if (format == "d")
            {
                return this.ToString(true, true);
            }
            return "a";
		}

		public string ToString(System.IFormatProvider provider)
		{
			return "a";
		}

		public PersianDate Subtract(TimeSpan value)
		{
			return new PersianDate(m_DateTime.Subtract(value));
		}

		public static PersianDate ParseExact ( string s , string[] formats , System.IFormatProvider provider , System.Globalization.DateTimeStyles style )
		{
			return new PersianDate(StringToDateTime(s));
		}

		public static PersianDate ParseExact ( string s , string format , System.IFormatProvider provider , System.Globalization.DateTimeStyles style )
		{
			return new PersianDate(StringToDateTime(s));
		}

		public static PersianDate ParseExact ( string s , string format , System.IFormatProvider provider )
		{
			return new PersianDate(StringToDateTime(s));
		}

		public TimeSpan Subtract(PersianDate value)
		{
			return m_DateTime.Subtract(value.m_DateTime);
		}

		public static PersianDate Today 
		{
			get {return new PersianDate(DateTime.Today);}
		}

		public PersianDate Add(System.TimeSpan value)
		{
			return new PersianDate(m_DateTime.Add(value));
		}

		public PersianDate AddDays(double value)
		{
			return new PersianDate(m_DateTime.AddDays(value));
		}

		public PersianDate AddHours(double value)
		{
			return new PersianDate(m_DateTime.AddHours(value));
		}

		public PersianDate AddMilliseconds(double value)
		{
			return new PersianDate(m_DateTime.AddMilliseconds(value));
		}

		public PersianDate AddMinutes(double value)
		{
			return new PersianDate(m_DateTime.AddMinutes(value));
		}

		public PersianDate AddMonths(int value)
		{
            if (true)
            {
                int year, month, day;
                GetJalaliDate(m_DateTime, out year, out month, out day);

                month += (year - 1) * 12 + value;
                year = (month - 1) / 12 + 1;
                month -= (year - 1) * 12;
                if ((month > 6) & (month < 12) & (day == 31)) day = 30;
                if ((month == 12) & (day > 29))
                {
                    if (IsLeapYear(year) == true) day = 30;
                    else day = 29;
                }

                return new PersianDate(year, month, day);
            }
            else
                return new PersianDate(m_DateTime.AddMonths(value));
		}

		public PersianDate AddSeconds(double value)
		{
			return new PersianDate(m_DateTime.AddSeconds(value));
		}

		public PersianDate AddTicks(long value)
		{
			return new PersianDate(m_DateTime.AddTicks(value));
		}

		public PersianDate AddYears(int value)
		{
			return new PersianDate(m_DateTime.AddYears(value));
		}

		public long ToFileTime()
		{
			return m_DateTime.ToFileTime();
		}

		public long ToFileTimeUtc()
		{
			return m_DateTime.ToFileTimeUtc();
		}

		public PersianDate ToLocalTime()
		{
			return new PersianDate(m_DateTime.ToLocalTime());
			
		}

		public string ToLongDateString()
		{
			return "a";
		}

		public string ToLongTimeString()
		{
			return "a";
		}

		public double ToOADate()
		{
			return m_DateTime.ToOADate();
		}

		public string ToShortDateString()
		{
            return this.ToString(true, false);
        }

		public string ToShortTimeString()
		{
            return this.ToString(false, true);
        }

		public PersianDate ToUniversalTime()
		{
			return new PersianDate(m_DateTime.ToUniversalTime());
		}
		

		public int Day
		{
			get 
			{
                if (true)
                {
                    int year, month, day;
                    GetJalaliDate(m_DateTime, out year, out month, out day);
                    return day;
                }
                else
                    return m_DateTime.Day;
			}
		}

		public int Year
		{
			get 
			{
                if (true)
                {
                    int year, month, day;
                    GetJalaliDate(m_DateTime, out year, out month, out day);
                    return year;
                }
                else
                    return m_DateTime.Year;
			}
		}

		public int Month
		{
			get 
			{
                if (true)
                {
                    int year, month, day;
                    GetJalaliDate(m_DateTime, out year, out month, out day);
                    return month;
                }
                else
                    return m_DateTime.Month;

			}
		}

		public DayOfWeek DayOfWeek
		{
			get {return m_DateTime.DayOfWeek;}
		}

        public int GetDayOfWeek()
        {
            int retval=0;

            if (true)
            {
                switch (m_DateTime.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                        retval = 0;
                        break;
                    case DayOfWeek.Sunday:
                        retval = 1;
                        break;
                    case DayOfWeek.Monday:
                        retval = 2;
                        break;
                    case DayOfWeek.Tuesday:
                        retval = 3;
                        break;
                    case DayOfWeek.Wednesday:
                        retval = 4;
                        break;
                    case DayOfWeek.Thursday:
                        retval = 5;
                        break;
                    case DayOfWeek.Friday:
                        retval = 6;
                        break;
                }
            }
            else
                retval = (int)m_DateTime.DayOfWeek;

            return retval;
        }

        private int GetDayOfYear(DateTime time)
		{
            if (true)
            {
                int d = (int)(time.Ticks / ticksPerDay) - 226529;
                d %= daysIn33Years;
                for (int i = 0; i < 33; i++)
                {
                    int daysInYear = 365 + leapFlag[i];
                    if (d < daysInYear)
                        return d + 1;
                    d -= daysInYear;
                }
                throw new ArgumentOutOfRangeException("time");
            }
            else
                return time.DayOfYear;
		}

		public PersianDate Date 
		{
			get{	return this;}
		}

		public int Hour
		{
			get { return m_DateTime.Hour;}
		}

		public int Minute
		{
			get { return m_DateTime.Minute;}
		}

		public int Millisecond
		{
			get { return m_DateTime.Millisecond;}
		}

		public int Second
		{
			get { return m_DateTime.Second;}
		}

		public long Ticks
		{
			get { return m_DateTime.Ticks;}
		}

		public TimeSpan TimeOfDay
		{
			get { return m_DateTime.TimeOfDay;}
		}

		public int DayOfYear
		{
			get { return GetDayOfYear(m_DateTime);}
		}

        public static string DateTimeToString(DateTime datetime)
        {
            if (true)
            {
                string s;
                int y;
                int m;
                int d;
                GetJalaliDate(datetime, out y, out m, out d);

                string month = "";
                string day = "";

                if (m < 10)
                    month = "0";

                month += m.ToString();

                if (d < 10)
                    day = "0";

                day += d.ToString();

                s = datetime.ToString("HH:mm") + " " + y.ToString() + "/" + month + "/" + day;
                return s;
            }
            else
            {
                return datetime.ToString("yyyy/MM/dd HH:mm");
            }
        }

		private static void GetJalaliDate(DateTime time, out int year, out int month, out int day)
		{
            if (true)
            {
                int d = (int)(time.Ticks / ticksPerDay) - 226529;
                year = (d / daysIn33Years) * 33;
                d %= daysIn33Years;
                for (int i = 0; i < 33; i++)
                {
                    int daysInYear = 365 + leapFlag[i];
                    if (d < daysInYear)
                        break;
                    d -= daysInYear;
                    year++;
                }
                if (d < 186)
                {
                    month = 1 + d / 31;
                    day = 1 + d % 31;
                }
                else
                {
                    d -= 186;
                    month = 7 + d / 30;
                    day = 1 + d % 30;
                }
            }
            else
            {
                year = time.Year;
                month = time.Month;
                day = time.Day;
            }
		}

		private static DateTime GetGregorianDate(int year,int month ,int day) 
		{
            if (true)
            {

                int iJYear = year;
                int iJMonth = month;
                int iJDay = day;

                //Continue
                int iTotalDays, iGYear, iGMonth, iGDay;
                int Div4, Div100, Div400;
                int iGDays;
                int i, leap;

                iTotalDays = JalaliDays(iJYear, iJMonth, iJDay);
                iTotalDays = iTotalDays + GYearOff;
                iGYear = (int)(iTotalDays / (Solar - 0.25 / 33));

                Div4 = iGYear / 4;
                Div100 = iGYear / 100;
                Div400 = iGYear / 400;

                iGDays = iTotalDays - (365 * iGYear) - (Div4 - Div100 + Div400);
                iGYear = iGYear + 1;

                if (iGDays == 0)
                {
                    iGYear--;
                    if (GLeap(iGYear) == 1)
                    {
                        iGDays = 366;
                    }
                    else
                    {
                        iGDays = 365;
                    }
                }
                else
                {
                    if (iGDays == 366 && GLeap(iGYear) != 1)
                    {
                        iGDays = 1;
                        iGYear++;
                    }
                }

                leap = GLeap(iGYear);
                for (i = 0; i <= 12; i++)
                {
                    if (iGDays <= GDayTable[leap, i])
                    {
                        break;
                    }

                    iGDays = iGDays - GDayTable[leap, i];
                }

                iGMonth = i + 1;
                iGDay = iGDays;

                return new DateTime(iGYear, iGMonth, iGDay);
            }
            else
            {
                return new DateTime(year, month, day);
            }
		}
		private static int JalaliDays(int iJYear,int iJMonth,int iJDay) 
		{

			//Calculate total days of jalali years from the base calendar
			int iTotalDays, iLeap;

			iLeap = JLeap(iJYear);
			for (int i=0; i<iJMonth-1; i++) 
			{
				iJDay = iJDay + JDayTable[iLeap,i];
			}

			iLeap = JLeapYears(iJYear - 1);
			iTotalDays = ((iJYear - 1) * 365 + iLeap + iJDay);

			return iTotalDays;
		}

		private static int GLeap(int GregYear) 
		{
			//Is gregorian year a leap year?
			int Mod4,Mod100,Mod400;
					
			Math.DivRem(GregYear,4,out Mod4);
			Math.DivRem(GregYear,100,out Mod100);
			Math.DivRem(GregYear,400,out Mod400);

			if (((Mod4 == 0) && (Mod100 != 0)) || (Mod400 == 0)) 
			{
				return 1;
			} 
			else 
			{
				return 0;
			}
		}

		private static int JLeap(int iJYear) 
		{
			//Is jalali year a leap year?
			int tmp;

			Math.DivRem(iJYear,33,out tmp);
			if ((tmp == 1) || (tmp == 5) || (tmp == 9) || (tmp == 13) || (tmp == 17) || (tmp == 22) || (tmp == 26) || (tmp == 30)) 
			{
				return 1;
			} 
			else 
			{
				return 0;
			}
		}

		private static int JLeapYears(int iJYear) 
		{
			
			int iLeap,iCurrentCycle, Div33;
			int iCounter;

			Div33 = iJYear / 33;
			iCurrentCycle = iJYear - (Div33 * 33);
			iLeap = (Div33 * 8);
			if (iCurrentCycle > 0) 
			{
				for (iCounter=1;iCounter<=18;iCounter=iCounter+4)
				{
					if(iCounter>iCurrentCycle) 
						break;
					iLeap++;
				}
			}
			if (iCurrentCycle > 21) 
			{
				for (iCounter=22;iCounter<=31;iCounter=iCounter+4) 
				{
					if(iCounter>iCurrentCycle)
						break;
					iLeap++;
				}
			
			}
			return iLeap;
		}


	}
}
