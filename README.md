# LIBRARYAPI

## Overview

`LIBRARYAPI` is a C# library that implements core functionalities for a content management system (CMS). It provides capabilities for managing posts, authors, readers, and interactions between them.

## Features

- **Post Management:**
  - Create, update, publish, delete posts.
  - Add and manage tags for posts.
  
- **Author Management:**
  - Register, update, and remove authors.
  - Create and manage draft posts.

- **Reader Management:**
  - Follow authors to receive updates on new posts.
  - Follow tags to see related posts.

- **Content Engine:**
  - Central component for coordinating interactions between authors, readers, and posts.
  - Ensures validity and consistency of content management operations.

- **Data Context:**
  - In-memory database component for CRUD operations on posts and users.

## Getting Started

To run and test `LIBRARYAPI`, follow these steps:

### Prerequisites

- [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/LIBRARYAPI.git

2. Navigate to the project directory, build and run tests:

   ```bash
   cd LIBRARYAPI
   dotnet build
   dotnet test
   
## Usage I have implemented the following possible commands to use the console API.
Usage:
    createauthor <authorName>
    createreader <readerName>
    createpost <authorName> <content>
    publishpost <authorName> <postIndex>
    deletepost <authorName> <postIndex>
    followtag <readerName> <tag>
    followauthor <readerName> <authorName>
    getfollowedposts <readerName>
    showauthors
    showposts <authorName>
    showreaders
    assigntag <authorName> <postIndex> <tag>

### Aknowledgements:
Powered by ChatGPT for assistance in crafting and refining project structure and documentation.
