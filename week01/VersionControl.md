## What is version control and why is it important?

<ul>
    <li>Explain the meaning of Version Control;</li>
    <li>Highlight a benefit of Version Control;</li>
    <li>Example of version control: Describe the way version control might be used on a software development team;</li>
    <li>Show a command used in Version Control (for example a Git command);</li>
    <li>Thoroughly explain these concepts (this likely cannot be done in less than 100 words);</li>
</ul>

R: Version Control Systems (VCS) are ways for developers and development teams to keep track of their progress on a project. Not only that, VCS also permit access to and recovery of an early version of a project if needed.

VCS allows developers to review the project history, allowing us to see which changes were made, who made them, when and where those changes were made and with the commit messages we can see why the changes were needed.

VCS are particularly useful when a team is working on a project because it allows multiple people to collaborate in the same project in at any time. This is commonly done by creating branches from the main project so developers can add new features, propose bug fixes, or simply implement changes that you or your team deem necessary to the code, and do Pull Requests (PR) for each major change, that after some review from the repo owner or Quality Control team those changes may or may not be accepted.

To illustrate how version control facilitates team collaboration in practice, here is a typical workflow that demonstrates the power and efficiency of this approach:

<ol>
    <li>Fork the project to your github account;</li>
    <li>Clone the repo to your machine with: <code>git clone repo_url
    </code></li>
    <li>Create a branch and switch to it to get the job done <code>git switch -c new_feature/solve_bug</code>;</li>
    <li>Repeat this step for each major change</li>
    <ul>
        <li>Do the programming to create the feature/solve the bug;</li>
        <li>Check your work with <code>git status</code> to see which files have been modified;</li>
        <li>Add files to be pushed with <code>git add file_changed.filetype</code>;</li>
        <li>Commit changes with <code>git commit -m "change made on x and y file to add feature/solve bug"</code>;</li>
        <li>Push changes to online repo with <code>git push origin branch</code>;</li>
    </ul>
    <li>Create a PR on the original owner repo main branch from your repo feature (PRs enable code review, quality control, and team discussion before merging changes);</li>
    <li>If the original repo owner/team accepts your contribution, you can merge/rebase your branch to the main project;</li>
</ol>

From my personal experience, VCS truly saved my sanity and productivity during my WDD131 final project last term. For every major change, I followed the recommended workflow: branching from each stable version of the site to work on new features or fixes. If the result met both my quality standards and my wife's (who acted as my personal designer), I would rebase the changes into the main branch. Otherwise, I would simply discard the branch and try again, sometimes using cherry-pick to keep useful commits. This approach proved especially valuable as the deadline approached—when a set of changes broke the code and I couldn't quickly identify the cause, I was able to revert to the last stable version and continue working without losing significant progress.

In essence, version control systems are indispensable tools that not only preserve the history and integrity of our code but also enable seamless collaboration, provide safety nets for experimentation, and ultimately make software development both more efficient and less stressful for individual developers and teams alike.
