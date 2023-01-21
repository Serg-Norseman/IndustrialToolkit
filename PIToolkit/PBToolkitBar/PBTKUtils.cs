using System;
using PBObjLib;

namespace PBToolkitBar
{
    public delegate void ProcessSymbolHandler(Symbol sym);

    /// <summary>
    /// 
    /// </summary>
    public static class PBTKUtils
    {
        public static void ProcessSymbol(Symbol sym, ProcessSymbolHandler handler)
        {
            try {
                if (sym.Type == (int)PBObjLib.pbSYMBOLTYPE.pbSymbolComposite) {

                    Composite comp = (Composite)sym;
                    for (int i = 1; i <= comp.GroupedSymbols.Count; i++) {
                        Symbol subsym = comp.GroupedSymbols.Item(i);
                        ProcessSymbol(subsym, handler);
                    }

                } else {

                    handler(sym);

                }
            } catch (Exception ex) {
                //MessageBox.Show(ex.Message);
            }
        }

        public static void ProcessSymbols(Symbols syms, ProcessSymbolHandler handler)
        {
            for (int i = 1; i <= syms.Count; i++) {
                try {
                    Symbol sym = syms.Item(i);
                    ProcessSymbol(sym, handler);
                } catch (Exception ex) {
                    //MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
