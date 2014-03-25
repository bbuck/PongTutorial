# Information

This is a project used in a tutorial series (currently in development) that I
will be putting up on my blog. In the meantime, this is the source for the
original finished game that will be the outcome of the tutorial. If you would
like to pull this down and use it in your own project to look at/experiement
with then feel free to do so.

This project is **public domain** in so much as nobody owns the source code so
if you see something you like feel free to use it for whatever you desire.

## Setting Up A New Project

There are two ways to set up a new project using this repository, one method is
with the `git` command line tool (which can be found [here](git-scm.com)) or by
downloading the most the latest marked release in `.zip` or `.tar.gz` format.

I highly recommend using `git` to perform this action and if you're not familair
with the `git` utility you can learn about it at [try.github.com](https://try.github.com).
At the very least you may find this as a useful tool for working with other
people on projects.

### Using Git

Perform a `cd` into whichever folder you wish to store your Unity Project, for
this example I'll use `/my/projects`.

```
$ cd /my/projects

If you have git setup over SSH
$ git clone git@github.com:bbuck/PongTutorial

Otherwise use HTTPS
$ git clone https://github.com/bbuck/PongTutorial.git
```

This will create a `PongTutorial` folder in your projects folder and you can
open this project with Unity.

### Downloading a Release

Chose the latest release from the release section found on this page and then
unarchive whichever format you downloaded and put the unarchived folder into
your projects folder, you might want to rename it from `PongTutorial-<release>`
to just `PongTutorial` or any other name you wish to use.

## Additional Note

This is a Unity project that is using a source control management (SCM) tool (`git`).
If you're setting up a different project to use a SCM tool then you need to
make sure the project is configured to easily integrate with the tool. To do
this, click Edit > Project Settings > Editor and make sure that the **Mode** under
**Version Control** is set to _Visible Meta Files_ as well as the **Mode** for
**Asset Serialization** set to _Force Text_. SCM tools do not handle binary files
very well when they've been modified in two different places so using text is
the best approach to making the project play nice with the tool to prevent any
unecessary problems.