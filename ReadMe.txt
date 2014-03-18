Simple membership provider.
Los campos extras no solamente hay que agregarlos a UserProfile, sino también a la database (manualmente).
http://www.codeproject.com/Articles/689801/Understanding-and-Using-Simple-Membership-Provider

Simple membership provider - ready to use admin pages (nuget)
http://www.mvccentral.net/Story/Details/tools/kahanu/securityguard-nuget-package-for-asp-net-membership

HTTPS/Self signed certificates
http://weblogs.asp.net/scottgu/archive/2007/04/06/tip-trick-enabling-ssl-on-iis7-using-self-signed-certificates.aspx
	Si da error y no se puede activar (porque el proceso esta en uso): http://support.microsoft.com/kb/890015


Simple membership provider and entity framework
	http://stackoverflow.com/questions/11382783/entity-framework-code-first-many-to-many-mapping-table


If files are removed from outside SVN (when deleting them from the Solution explorer without VisualSVN/Exclude...), go to the root directory of the solution and run this in a PowerShell (not cmd)
svn status | ? { $_ -match '^!\s+(.*)' } | % { svn rm $Matches[1] }