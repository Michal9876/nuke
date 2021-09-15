// Copyright 2019 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Nuke.Common.Git;
using Xunit;

namespace Nuke.Common.Tests
{
    public class GitRepositoryTest
    {
        [Theory]
        [InlineData("https://github.com/nuke-build", "github.com", "nuke-build", null)]
        [InlineData("https://github.com/nuke-build/", "github.com", "nuke-build", null)]
        [InlineData("https://github.com/nuke-build/nuke", "github.com", "nuke-build/nuke", null)]
        [InlineData("https://github.com/nuke-build/nuke.git", "github.com", "nuke-build/nuke", null)]
        [InlineData("https://user:pass@github.com/nuke-build/nuke.git", "github.com", "nuke-build/nuke", null)]
        [InlineData(" https://github.com/TdMxm/nuke.git", "github.com", "TdMxm/nuke", null)]
        [InlineData("git@git.test.org:test", "git.test.org", "test", null)]
        [InlineData("git@git.test.org/test", "git.test.org", "test", null)]
        [InlineData("git@git.test.org/test/", "git.test.org", "test", null)]
        [InlineData("git@git.test.org/test.git", "git.test.org", "test", null)]
        [InlineData("ssh://git@git.test.org/test.git", "git.test.org", "test", null)]
        [InlineData("ssh://git@git.test.org:1234/test.git", "git.test.org", "test", 1234)]
        [InlineData("ssh://git.test.org/test/test", "git.test.org", "test/test", null)]
        [InlineData("ssh://git.test.org:1234/test/test", "git.test.org", "test/test", 1234)]
        [InlineData("https://git.test.org:1234/test/test", "git.test.org", "test/test", 1234)]
        [InlineData("git://git.test.org:1234/test/test", "git.test.org", "test/test", 1234)]
        [InlineData("git://git.test.org/test/test", "git.test.org", "test/test", null)]
        public void FromUrlTest(string url, string endpoint, string identifier, int? port)
        {
            var repository = GitRepository.FromUrl(url);
            repository.Endpoint.Should().Be(endpoint);
            repository.Identifier.Should().Be(identifier);
            repository.Port.Should().Be(port);
        }

        [Theory]
        [InlineData("https://github.com/nuke-build", GitProtocol.Https)]
        [InlineData("git@git.test.org:test", GitProtocol.Ssh)]
        [InlineData("ssh://git.test.org:1234/test/test", GitProtocol.Ssh)]
        [InlineData("git://git.test.org:1234/test/test", GitProtocol.Ssh)]
        public void FromUrlProtocolTest(string url, GitProtocol protocol)
        {
            var repository = GitRepository.FromUrl(url);
            repository.Protocol.Should().Be(protocol);
        }

        [Fact]
        public void FromDirectoryTest()
        {
            var repository = GitRepository.FromLocalDirectory(Directory.GetCurrentDirectory()).NotNull();
            repository.Endpoint.Should().NotBeNullOrEmpty();
            repository.Identifier.Should().NotBeNullOrEmpty();
            repository.LocalDirectory.Should().NotBeNullOrEmpty();
            repository.Head.Should().NotBeNullOrEmpty();
            repository.Commit.Should().NotBeNullOrEmpty();
            repository.Tags.Should().NotBeNull();
            repository.Port.Should().BeNull();
        }
    }
}
