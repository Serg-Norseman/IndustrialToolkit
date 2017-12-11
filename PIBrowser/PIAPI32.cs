using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PIBrowser
{
    public enum _PIvaluetype : byte
    {
        PI_Type_null,
        PI_Type_bool,
        PI_Type_uint8,
        PI_Type_int8,
        PI_Type_char,
        PI_Type_uint16,
        PI_Type_int16,
        PI_Type_uint32,
        PI_Type_int32,
        PI_Type_uint64,
        PI_Type_int64,
        PI_Type_float16,
        PI_Type_float32,
        PI_Type_float64,
        PI_Type_PI2,
        PI_Type_digital = 101,
        PI_Type_blob,
        PI_Type_PItimestamp = 104,
        PI_Type_PIstring,
        PI_Type_bad = 255
    }

    public static class PIAPI32
    {
        public struct QERROR
        {
            public int Point;
            public int piapierror;
        }

        public struct QERRORS
        {
            public int syserror;
            public int numpterrs;
            public QERROR[] qerr;
        }

        public struct PI_EXCEPT
        {
            public float NewVal;
            public int newstat;
            public int newTime;
            public float oldVal;
            public int oldstat;
            public int oldTime;
            public float prevVal;
            public int prevstat;
            public int prevTime;
            public float ExcDevEng;
            public int ExcMin;
            public int ExtMax;
        }

        public struct PI_VAL
        {
            public int bsize;
            public int istat;
            public int flags;
        }

        public struct TagList
        {
            public string server;
            public int NodeID;
            public string tagname;
            public int Point;
            public int reserved;
        }

        public struct PITimeStamp
        {
            public int Month;
            public int Year;
            public int Day;
            public int Hour;
            public int Minute;
            public int tzinfo;
            public double Second;
        }

        #region PI batch function declarations -- New with PI-API v1.1.0

        [DllImport("piapi32.dll")]
        public static extern int piba_getunit(out string unit, int len, int index, out int number);

        [DllImport("piapi32.dll")]
        public static extern int piba_getaliaswunit(string unit, out string alias, int len, int index, out int number);

        [DllImport("piapi32.dll")]
        public static extern int piba_getunitswalias(string alias, ref string unit, int len, int index, out int number);

        [DllImport("piapi32.dll")]
        public static extern int piba_findaliaspoint(string unit, string alias, out int point, out int tagname, int len);

        [DllImport("piapi32.dll")]
        public static extern int piba_search(ref string batchid, int len0, ref string unit, int len1, ref string product,
                                             int len2, ref string starttime, ref string endtime, int searchflag, int timeout);

        #endregion

        #region PI Login Services function declarations -- new with PI-API v1.1.0

        [DllImport("pilog32.dll")]
        public static extern int pilg_addnewserver(ref string lpszServerName, uint nServerType,
                                                   ref string lpszUserName, int nPortNum);

        [DllImport("pilog32.dll")]
        public static extern int pilg_connectdlg(int hWndParent);

        [DllImport("pilog32.dll")]
        public static extern int pilg_disconnect();

        [DllImport("pilog32.dll")]
        public static extern int pilg_disconnectnode(ref string lpszServerName);

        [DllImport("pilog32.dll")]
        public static extern int pilg_getconnectedserver(ref string lpszServerNameBuf, ref int plBufSize,
                                                         out int plNodeId, out int piPort, int ISeq);

        [DllImport("pilog32.dll")]
        public static extern int pilg_getdefserverinfo(out string lpszServerNameBuf, ref int plBufSize,
                                                       out int plNodeId, out int plPort);

        [DllImport("pilog32.dll")]
        public static extern int pilg_getnodeid(ref string lpszServerNameBuf, out int plNodeId);

        [DllImport("pilog32.dll")]
        public static extern int pilg_getselectedtag(ref string lpcTagList, int lSeq);

        [DllImport("pilog32.dll")]
        public static extern int pilg_getservername(int LNodeId, out string lpszServerNamdBuf, ref int plBufSize);

        [DllImport("pilog32.dll")]
        public static extern int pilg_login(int hWndParent, string lpszUserName, string lpszServerName,
                                            string lpszPassWord, out int valid);

        [DllImport("pilog32.dll")]
        public static extern int piba_findaliaspoint(int hWndParent, TagList lptglstHead);

        /// <summary>
        /// This function registers the application with the PILOG32.DLL and should be called before calling any of the other PILOG32.DLL functions
        /// </summary>
        /// <param name="lpszDLLName"></param>
        /// <returns></returns>
        [DllImport("pilog32.dll")]
        public static extern int pilg_registerapp(string lpszDLLName);

        [DllImport("pilog32.dll")]
        public static extern int pilg_registerhelp(string lpszHelpFile, string lplstHelpId);

        [DllImport("pilog32.dll")]
        public static extern int pilg_setservernode(string lpszServerName);

        [DllImport("pilog32.dll")]
        public static extern int pilg_tagsearchdlg(int hWndParent);

        [DllImport("pilog32.dll")]
        public static extern int pilg_unregisterapp();

        #endregion

        #region Function declarations

        [DllImport("piapi32.dll")]
        public static extern int piar_calculation(ref int count, ref int[] times, out float[] rvals,
                                                  out int[] istats, string calcstr);

        [DllImport("piapi32.dll")]
        public static extern int piar_compvalues(int pt, ref int count, ref int[] times, out float[] rvals,
                                                 out int[] istats, int rev);


        [DllImport("piapi32.dll")]
        public static extern int piar_compvaluesfil(int pt, ref int count, ref int[] times, out float[] rvals,
                                                    out int[] istats, string expression, int rev, int filt);

        [DllImport("piapi32.dll")]
        public static extern int piar_deletevalue(int pt, int timedate);

        [DllImport("piapi32.dll")]
        public static extern int piar_interpvalues(int pt, ref int count, ref int[] times,
                                                   out float[] rvals, out int[] istats);

        [DllImport("piapi32.dll")]
        public static extern int piar_interpvaluesfil(int pt, ref int count, ref int[] times,
                                                      out float[] rvals, out int[] istats);

        [DllImport("piapi32.dll")]
        public static extern int piar_panvalues(int pt, ref int coung, ref int timedate);

        [DllImport("piapi32.dll")]
        public static extern int piar_plotvalues(int pt, int intervals, ref int count, ref int[] times,
                                                 out float[] rvals, out int[] istats);

        [DllImport("piapi32.dll")]
        public static extern int piar_putvalue(int pt, float rval, int istat, int timedate, int wait);

        [DllImport("piapi32.dll")]
        public static extern int piar_replacevalue(int pt, int timedate, float rval, int istat);

        [DllImport("piapi32.dll")]
        public static extern int piar_summary(int pt, ref int time1, ref int time2, out float rval,
                                              out float pctgood, int code);

        [DllImport("piapi32.dll")]
        private unsafe static extern int piar_timedvalues(int pt, ref int count, int* times,
                                                          float* rvals, int* istats, int prev);

        public unsafe static int piar_timedvaluesex(int pt, ref int count, ref int[] times,
                                                    out float[] rvals, out int[] istats, int prev)
        {
            rvals = new float[count];
            istats = new int[count];
            int result;
            fixed (int* tPtr = times) {
                fixed (float* rvPtr = rvals) {
                    fixed (int* isPtr = istats) {
                        result = piar_timedvalues(pt, ref count, tPtr, rvPtr, isPtr, prev);
                    }
                }
            }
            return result;
        }

        [DllImport("piapi32.dll")]
        public static extern int piar_timedvaluesfil(int pt, ref int count, ref int[] times,
                                                     out float[] rvals, out int[] istats, string expression);

        [DllImport("piapi32.dll")]
        public static extern int piar_timefilter(int starttime, int endtime, string expression,
                                                 out int tottime, out int passtime);

        [DllImport("piapi32.dll")]
        public static extern int piar_value(int pt, ref int timedate, int mode, out float rval, out int istat);

        [DllImport("piapi32.dll")]
        public static extern int piel_addevnt(ref int time, out int number, int group, int type, string msg, int timeout);

        [DllImport("piapi32.dll")]
        public static extern int piel_evntactn(ref int time, ref int number, ref int group, ref int type, int len, ref string msg, int action, int timeout);

        [DllImport("piapi32.dll")]
        public static extern int pilg_checklogfile(int action, string logfile);

        [DllImport("piapi32.dll")]
        public static extern int pilg_formputlog(string msg, string idstring);
        
        [DllImport("piapi32.dll")]
        public static extern int pilg_puthomelog(string msg);

        [DllImport("piapi32.dll")]
        public static extern int pilg_putlog(string msg);

        [DllImport("piapi32.dll")]
        public static extern int pilg_putoutput(string msg, int flags);

        [DllImport("piapi32.dll")]
        public static extern int pipt_compspecs(int pt, out int compdev, out int compmin, out int compmax);

        [DllImport("piapi32.dll")]
        public static extern int pipt_compspecseng(int pt, out float compdev, out int compmin, out int compmax);

        [DllImport("piapi32.dll")]
        public static extern int pipt_dates(int pt, out int creationdate, out string creator, int crlen, out int changedate, out string changer, int chlen);
        
        [DllImport("piapi32.dll")]
        public static extern int pipt_descriptor(int pt, out string descriptor, int len);

        [DllImport("piapi32.dll")]
        unsafe private static extern int pipt_descriptorx(int pt, byte* str, ref int len);

        public unsafe static int pipt_descriptorex(int pt, out string desc)
        {
            byte[] strbuf = new byte[256];
            fixed (byte* lpStr = strbuf) {
                int len = 255;
                int result = PIAPI32.pipt_descriptorx(pt, lpStr, ref len);
                desc = Encoding.UTF8.GetString(strbuf, 0, len);
                return result;
            }
        }

        [DllImport("piapi32.dll")]
        public static extern int pipt_digcode(out int digcode, string digstring);

        [DllImport("piapi32.dll")]
        public static extern int pipt_digcodefortag(int pt, out int digcode, string digstring);

        [DllImport("piapi32.dll")]
        public static extern int pipt_digpointers(int pt, out int digcode, out int dignumb);

        [DllImport("piapi32.dll")]
        public static extern int pipt_digstate(int digcode, out string digstate, int len);

        [DllImport("piapi32.dll")]
        public static extern int pipt_displaydigits(int pt, out int displaydigits);

        [DllImport("piapi32.dll")]
        public static extern int pipt_engunitstring(int pt, out string engunitstring, int len);

        [DllImport("piapi32.dll")]
        public static extern int pipt_engunstring(int engunitcode, out string engunitstring, int len);

        [DllImport("piapi32.dll")]
        public static extern int pipt_excspecseng(int pt, out float excdeveng, out int excmin, out int excmax);

        [DllImport("piapi32.dll")]
        public static extern int pipt_exdesc(int pt, out string exdesc, int len);

        [DllImport("piapi32.dll")]
        unsafe private static extern int pipt_findpoint(byte* tagname, out int pointnum);

        public unsafe static int pipt_findpointex(string tagname, out int pointnum)
        {
            byte[] btarr = Encoding.ASCII.GetBytes(tagname);
            int result;
            fixed (byte* bt = btarr)
            {
                result = pipt_findpoint(bt, out pointnum);
            }
            return result;
        }

        [DllImport("piapi32.dll")]
        public static extern int pipt_inprocbits(int pt, out int larchiving, out int lcompressing, out int filtercode);

        [DllImport("piapi32.dll")]
        public static extern int pipt_instrumenttag(int pt, out string instrumenttag, int len);

        [DllImport("piapi32.dll")]
        public static extern int pipt_location(int pt, out int[] location);

        [DllImport("piapi32.dll")]
        public static extern int pipt_nextptwsource(string source, ref int pt);

        [DllImport("piapi32.dll")]
        public static extern int pipt_pointid(int pt, out int ipt);

        [DllImport("piapi32.dll")]
        public static extern int pipt_pointsource(int pt, out string source);

        [DllImport("piapi32.dll")]
        public static extern int pipt_pointtype(int pt, out char type);

        [DllImport("piapi32.dll")]
        public static extern int pipt_ptexist(int pt);

        [DllImport("piapi32.dll")]
        public static extern int pipt_recordtype(int pt, out int steps);

        [DllImport("piapi32.dll")]
        public static extern int pipt_rescode(int pt, out int rescode);

        [DllImport("piapi32.dll")]
        public static extern int pipt_scale(int pt, out float zero, out float span);

        [DllImport("piapi32.dll")]
        public static extern int pipt_scan(int pt, out int lscan);

        [DllImport("piapi32.dll")]
        public static extern int pipt_signupforupdates();

        [DllImport("piapi32.dll")]
        public static extern int pipt_sourcept(int pt, out int sourcept);

        [DllImport("piapi32.dll")]
        public static extern int pipt_squareroot(int pt, out int squareroot);

        [DllImport("piapi32.dll")]
        public static extern int pipt_tag(int pt, out string tag, int len);

        [DllImport("piapi32.dll")]
        public static extern int pipt_taglong(int pt, out string tagname, int len);

        [DllImport("piapi32.dll")]
        public static extern int pipt_tagpreferred(int pt, out string tagname, int len);

        [DllImport("piapi32.dll")]
        public static extern int pipt_totalspecs(int pt, out int totalcode, out float convers);

        [DllImport("piapi32.dll")]
        public static extern int pipt_typicalvalue(int pt, out float typicalvalue);

        [DllImport("piapi32.dll")]
        public static extern int pipt_updates(out int pt, out string tagname, int len, out int mode);

        [DllImport("piapi32.dll")]
        public static extern int pipt_userattribs(int pt, out int Userint1, out int Userint2, out float Userreal1, out float Userreal2);

        [DllImport("piapi32.dll")]
        public unsafe static extern int pipt_wildcardsearch(byte* tagmask, int direction,
                                                            out int found, byte* tagname,
                                                            int len, out int pt, out int numfound);

        public unsafe static int pipt_wildcardsearchex(string tagmask, int direction,
                                                       out int found, out string tagname,
                                                       out int pt, out int numfound)
        {
            int len = 255;
            byte[] tm_buf = Encoding.ASCII.GetBytes(tagmask);
            int result;
            fixed (byte* bt = tm_buf) {
                byte[] strbuf = new byte[256];
                fixed (byte* lpStr = strbuf) {
                    result = pipt_wildcardsearch(bt, direction,
                                                 out found, lpStr,
                                                 len, out pt, out numfound);
                    tagname = Encoding.UTF8.GetString(strbuf).TrimEnd((Char)0);
                }
            }
            return result;
        }

        [DllImport("piapi32.dll")]
        public static extern int pisn_evmdisestablish(ref int count, int pts);

        [DllImport("piapi32.dll")]
        public static extern int pisn_evmestablish(ref int count, int pts);

        [DllImport("piapi32.dll")]
        public static extern int pisn_getsnapshot(int pt, out float rval, out int istat, out int timedate);

        [DllImport("piapi32.dll")]
        public static extern int pisn_getsnapshots(int[] pt, out float[] rval, out int[] istat, out int[] timedate, out int[] error, int count);

        [DllImport("piapi32.dll")]
        public static extern int pisn_putsnapshot(int pt, float rval, int istat, int timedate);

        [DllImport("piapi32.dll")]
        public static extern int pisn_putsnapshots(int[] pt, float[] rval, int[] istat, int[] timedate, out int[] error, int count);

        [DllImport("piapi32.dll")]
        public static extern int pisn_sendexcepstruc(int pt, char type, ref PI_EXCEPT excpt, out int count);

        [DllImport("piapi32.dll")]
        public static extern int pisn_sendexceptions(int pt, char type, ref float oldval, ref int oldstat, ref int oldtime, ref float prevval, ref int prevstat, ref int prevtime, float newval, int newstat, int newtime, float excdeveng, int excmin, int excmax, out int count);

        [DllImport("piapi32.dll")]
        public static extern int pisn_sendexcepstrucq(int pt, char type, ref PI_EXCEPT excpt, out int count, bool queueing, out int numbpterrs, out QERROR[] qerrs);

        [DllImport("piapi32.dll")]
        public static extern int pisn_sendexceptionq(int pt, char type, ref float oldval, ref int oldstat, ref int oldtime, ref float prevval, ref int prevstat, ref int prevtime, float newval, int newstat, int newtime, float excdeveng, int excmin, int excmax, out int count);

        [DllImport("piapi32.dll")]
        public static extern int pisn_putsnapshotq(int pt, float rval, int istat, int timedate);

        [DllImport("piapi32.dll")]
        public static extern int pisn_flushputsnapq();

        [DllImport("piapi32.dll")]
        public static extern int pitm_delay(int mseconds);

        [DllImport("piapi32.dll")]
        public static extern int pitm_fastservertime();

        [DllImport("piapi32.dll")]
        unsafe public static extern void pitm_formtime(int timedate, byte* timestring, int len);

        [DllImport("piapi32.dll")]
        public static extern void pitm_intsec(out int timedate, int[] timearray);

        [DllImport("piapi32.dll")]
        public static extern int pitm_parsetime(string str, int reltime, out int timedate);

        [DllImport("piapi32.dll")]
        public static extern void pitm_secint(int timedate, out int[] timearray);

        [DllImport("piapi32.dll")]
        public static extern int pitm_servertime(out int servertime);

        [DllImport("piapi32.dll")]
        public static extern int pitm_syncwithservertime();

        [DllImport("piapi32.dll")]
        public static extern int pitm_systime();

        [DllImport("piapi32.dll")]
        public static extern int piut_connect(string procname);

        [DllImport("piapi32.dll")]
        public static extern int piut_disconnect();

        [DllImport("piapi32.dll")]
        public static extern int piut_disconnectnode(string nodename);

        [DllImport("piapi32.dll")]
        public static extern int piut_fastserverversion(ref int minorver, string buildid, int buildlen);

        [DllImport("piapi32.dll")]
        public static extern int piut_getapiversion(ref int minorver, string buildid, int buildlen);

        [DllImport("piapi32.dll")]
        public static extern int piut_getloginuser(ref int minorver, string bulidid, int buildlen);

        [DllImport("piapi32.dll")]
        public static extern void piut_getprotocolvers(out string vers, int len);

        [DllImport("piapi32.dll")]
        public static extern int piut_getserverversion(ref int nodeid, string servername, int servernamelen, string versinon, int versionlen, string vuildid, int buildidlen);

        [DllImport("piapi32.dll")]
        public static extern void piut_inceventcounter(int counter, int count);

        [DllImport("piapi32.dll")]
        public static extern int piut_ishome();

        [DllImport("piapi32.dll")]
        public static extern int piut_login(string username, string passwrd, out int valid);

        [DllImport("piapi32.dll")]
        public static extern int piut_netinfo(out string name, int namelen, out string address, int addresslen, out string type, int typelen);

        [DllImport("piapi32.dll")]
        public static extern int piut_netnodeinfo(string name, int namelen, out string address, int addresslen, out int connected);

        [DllImport("piapi32.dll")]
        public static extern int piut_netserverinfo(out string name, int namelen, out string address, int addresslen, out int connected);

        [DllImport("piapi32.dll")]
        public static extern int piut_setdefaultservernode(string server);

        [DllImport("piapi32.dll")]
        public static extern void piut_setprocname(string procname);

        [DllImport("piapi32.dll")]
        public static extern int piut_setservernode(string servername);

        [DllImport("piapi32.dll")]
        public static extern void piut_zeroeventcounter(int counter);

        [DllImport("piapi32.dll")]
        public static extern int piut_isconnected();

        [DllImport("piapi32.dll")]
        public static extern int piar_getarcvaluesx(int ptnum, int arcmode, ref int count, out double drval, out int ival,
                                                    out object bval, ref int bsize, out int istat, out int flags, PITimeStamp time0, ref PITimeStamp time1, int funccode);

        [DllImport("piapi32.dll")]
        public static extern int piar_getarcvaluex(int ptnum, int mode, out double drval, out int ival, out object bval, ref int bsize,
                                                   out int istat, out int flags, out PITimeStamp time);

        [DllImport("piapi32.dll")]
        public static extern int piar_putarcvaluesx(int count, int mode, int[] ptnum, double[] drval, int[] ival, object[] bval,
                                                    int[] bsize, int[] istat, int[] flags, PITimeStamp[] timestamp, out int[] errors);

        [DllImport("piapi32.dll")]
        public static extern int piar_putarcvaluex(int ptnum, int mode, double drval, int ival, object bval, object bsize, int istat, int flags, PITimeStamp[] timestamp);

        [DllImport("piapi32.dll")]
        public static extern int pipt_pointtypex(int ptnum, out _PIvaluetype typex);

        //[DllImport("piapi32.dll")]
        //public static extern int pisn_evmexceptx()ref int count,out

        [DllImport("piapi32.dll")]
        public static extern int pisn_flushputsnapqx(out int[] numbpterrs, out QERROR[] qerrrs);

        [DllImport("piapi32.dll")]
        public static extern int pisn_getsnapshotsx(int[] ptnums, ref int count_ptnum, out double[] drval, out int[]ival, out object[] bval, ref int[] bsize, out int[] istat, out int[] flags, out PITimeStamp[] times, out int[] errors, int funccode);

        [DllImport("piapi32.dll")]
        public static extern int pisn_getsnapshotx(int ptnums, out double drval, out int ival, out object bval, ref int bsize, out int istat, out int flags, out PITimeStamp times);

        [DllImport("piapi32.dll")]
        public static extern int pisn_putsnapshotqx(int ptnum, double rval, int ival, object bval, int bsize, int istat, int flags, PITimeStamp times, bool queueing, out int numbpterrs, out QERROR qerrs);

        [DllImport("piapi32.dll")]
        public static extern int pisn_putsnapshotsx(int count, int[] ptnum, double[] drval, int[] ival, object[] bval, int[] bsize, int[] istat, int[] flags, PITimeStamp[] timestamp, out int[] errors);

        [DllImport("piapi32.dll")]
        public static extern int pisn_putsnapshotx(int ptnum, double drval, int ival, object bval, int bsize, int istat, int flags, PITimeStamp timestamp);

        [DllImport("piapi32.dll")]
        public static extern int pitm_getpitime(PITimeStamp timestamp, out double frac);

        [DllImport("piapi32.dll")]
        public static extern int pitm_getutctime(PITimeStamp timestamp);

        [DllImport("piapi32.dll")]
        public static extern int pitm_isdst(PITimeStamp timestamp);

        [DllImport("piapi32.dll")]
        public static extern void pitm_setcurtime(out PITimeStamp timestamp, bool incl_subsec);

        [DllImport("piapi32.dll")]
        public static extern void pitm_setdst(ref PITimeStamp timestamp, int tm_dst);

        [DllImport("piapi32.dll")]
        public static extern int pitm_setpitime(out PITimeStamp timestamp, int pitime, double frac);

        [DllImport("piapi32.dll")]
        public static extern int pitm_settime(out PITimeStamp timestamp, int year, int month, int day, int hour, int minute, double second);

        [DllImport("piapi32.dll")]
        public static extern int piut_errormsg(string nodename);

        [DllImport("piapi32.dll")]
        public static extern int piut_setpassword(string username, string oldpassword, string newpassword);

        [DllImport("piapi32.dll")]
        public static extern int piut_strerror(int stat, out string msg, ref int msglen, string filter);

        #endregion

        #region Helpers

        #endregion
    }
}
