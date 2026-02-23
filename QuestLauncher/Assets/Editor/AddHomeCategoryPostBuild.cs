using System.Xml;
using UnityEditor;
using UnityEditor.Android;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class AddHomeCategoryPostBuild : IPostGenerateGradleAndroidProject
{
    public int callbackOrder => 0;

    public void OnPostGenerateGradleAndroidProject(string path)
    {
        string manifestPath = path + "/src/main/AndroidManifest.xml";
        XmlDocument doc = new XmlDocument();
        doc.Load(manifestPath);

        XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
        ns.AddNamespace("android", "http://schemas.android.com/apk/res/android");

        // Adiciona CATEGORY_HOME
        XmlNode intentFilter = doc.SelectSingleNode(
            "//activity/intent-filter[action/@android:name='android.intent.action.MAIN']", ns);

        if (intentFilter != null)
        {
            XmlElement home = doc.CreateElement("category");
            home.SetAttribute("name", "http://schemas.android.com/apk/res/android", "android.intent.category.HOME");
            intentFilter.AppendChild(home);

            XmlElement def = doc.CreateElement("category");
            def.SetAttribute("name", "http://schemas.android.com/apk/res/android", "android.intent.category.DEFAULT");
            intentFilter.AppendChild(def);
        }

        // Adiciona permissão BOOT_COMPLETED
        XmlNode manifest = doc.SelectSingleNode("//manifest");
        XmlElement permission = doc.CreateElement("uses-permission");
        permission.SetAttribute("name", "http://schemas.android.com/apk/res/android", "android.permission.RECEIVE_BOOT_COMPLETED");
        manifest.AppendChild(permission);

        // Adiciona BroadcastReceiver
        XmlNode application = doc.SelectSingleNode("//application");
        XmlElement receiver = doc.CreateElement("receiver");
        receiver.SetAttribute("name", "http://schemas.android.com/apk/res/android", "com.hubduction.questlauncher.BootReceiver");
        receiver.SetAttribute("exported", "http://schemas.android.com/apk/res/android", "true");
        receiver.SetAttribute("enabled", "http://schemas.android.com/apk/res/android", "true");

        XmlElement bootFilter = doc.CreateElement("intent-filter");
        XmlElement bootAction = doc.CreateElement("action");
        bootAction.SetAttribute("name", "http://schemas.android.com/apk/res/android", "android.intent.action.BOOT_COMPLETED");
        bootFilter.AppendChild(bootAction);
        receiver.AppendChild(bootFilter);
        application.AppendChild(receiver);

        doc.Save(manifestPath);
    }
}