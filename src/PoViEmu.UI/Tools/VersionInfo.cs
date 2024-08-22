using System;
using TG = ThisAssembly.Git;
using TI = ThisAssembly.Info;

// ReSharper disable UnusedMember.Global

namespace PoViEmu.Tasty
{
    public sealed class VersionInfo
    {
        public string CommitMonth => FixDate(TG.CommitDate);

        public string RepoUrl => FixRepoUrl(TG.RepositoryUrl);

        public string Branch => TG.Branch;

        public string LastCommit => TG.Commit;

        public int CommitCount => FixCount(TG.Commits);

        public string Copyright => TI.Copyright;

        public string Description => TI.Description;

        public string Company => TI.Company;

        public string Config => TI.Configuration;

        public string Title => FixTitle(TI.Title);

        public string Product => TI.Product;

        public string Version => TI.Version;

        private static string FixDate(string date)
            => DateTime.Parse(date).ToString("MMM yyyy");

        private static string FixRepoUrl(string url)
            => url.Replace(':', '/').Replace("git@", "https://").Replace(".git", "");

        private static int FixCount(string commits)
            => int.Parse(commits);

        private static string FixTitle(string text)
            => text.Replace('.', ' ');
    }
}