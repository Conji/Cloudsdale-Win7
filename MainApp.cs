using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CloudsdaleWin7
{
    internal class MainApp
    {
        [STAThread]
        public static void Main()
        {
            ResolveAssembly();
            App.Main();
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        public static void ResolveAssembly()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);
            var path = assemblyName.Name + ".dll";
            if (!assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture))
            {
                path = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path);
            }

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                if (stream == null)
                    return null;

                var assemblyRawBytes = new byte[stream.Length];
                stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                return Assembly.Load(assemblyRawBytes);
            }
        }
    }
}
