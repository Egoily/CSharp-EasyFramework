﻿using System.Reflection;

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(EtakVersion.VersionString)]
[assembly: AssemblyInformationalVersion(EtakVersion.InformationalVersion)]
[assembly: AssemblyFileVersion(EtakVersion.VersionString)]

/// <summary>
/// Class used to update AssemblyVersion and FileVersion
/// </summary>
internal static class EtakVersion
{
	/// <summary>
    /// VersionString: Used to inform AssemblyVersion
    /// </summary>
	public const string VersionString = "2.0.12.0";
    /// <summary>
    /// It's a compound of VersionString (AssemblyVersion) and the Revision Number
    /// </summary>
	public const string InformationalVersion =  VersionString + "";
}