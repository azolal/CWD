# Introduction

This guide provides essential information about CWD Convertor and is
dedicated to familiarize the user with its operation. CWD Convertor
(hereinafter referred to as the „convertor") is an application meant to
help a user to find common characteristics in neighbouring genes. For
such purpose, the convertor extracts specific information from multiple
freely available sources, processes that information and provides
acquired results to user in a form of well-arranged table (hereinafter
referred to as the „report").

# Extraction of information

As mentioned above, the convertor uses multiple sources for obtaining
the necessary information. Those sources are as follows: Ontology files
(Gene Ontology Annotations and Open Biomedical Ontologies), Genome
Reference Consortium Human (GRCh38.p13), a file containing names of
randomly selected genes (CSV format) and a file containing official
symbols of selected genes for comparison (CSV format). The purpose of
each type of file is discussed in subsequent paragraphs.

## GOA -- Gene ontology annotation

The main purpose of GOA file is to provide the converter with
information of all genes within its database. The convertor then
extracts such information about specifically selected genes included in
files containing official symbols of selected genes for comparison
(discussed later in chapter 2.5). The information extracted from GOA
file are:

1.  Processes of a gene

2.  Components of a gene

3.  Functions of a gene

The GOA provides such information in a form of OBO codes.

## OBO -- Open biological and biomedical ontologies

Information about meanings of OBO codes is contained within this file.
The converter uses this file to translate provided OBO codes (by GOA
file) for the user in the final report.

## Genome reference consortium human 

Information about gene name, location, strand and neighbouring genes for
the final report is extracted from this file.

## Random gene file

As mentioned above, this software compares main genes to their
neighboring genes. To correctly evaluate obtained data, a control group
of 100 genes has to be established. This file contains so called
official symbols of those randomly selected genes. It is a simple list
in a form of a single column CSV file.

## File containing selected genes

This file provides the converter with official symbols of genes selected
for intended comparison. It is a simple list in a form of a single
column CSV file.

# Report structure

The report created by the convertor contains three sheets. The first
sheet is named after the main gene and contains the comparison of the
main gene to neighbouring genes (see Figure 1).

The second sheet is named again after the main gene with a suffix ,--
Random genes'. This sheet contains the comparison of main gene to the
above mentioned randomly selected control genes (see Figure 2).

The third sheet named once more after the main gene with a suffix ,--
ABCD' and contains the calculated variables a,b,c and d for further
statistical analysis (see Figure 3). Calculation of those variables is
discussed later in chapter 3.3.

Table 1: Meaning of colour codes used in sheet 1 and 2

<div align="center">
  Table 1: Meaning of colour codes used in sheet 1 and 2
  <br>
  <img src="https://user-images.githubusercontent.com/96398296/155196226-2873a304-3319-4160-acad-8492bd9d5250.png">
</div>


## Information in the first sheet

The first sheet gives user the compared information between the selected
main gene and its neighbouring genes, which are present in a certain
range on the DNA strand. Matching information of a main gene and a
neighbouring gene is highlighted in red colour. The first sheet includes
information about:

1.  Official symbol of a gene

2.  Official name of a gene

3.  Location of a gene

4.  DNA strand location of a gene

5.  Processes of a gene

6.  Components of a gene

7.  Functions of a gene

<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155212291-6d2451a3-38bb-4b33-a051-cd7e271d4437.png">

  Figure 1
</div>

## Information in the second sheet

The second sheet provides compared information between the selected main
gene and a control group of 100 randomly selected genes. Matching
information of a main gene and a random gene is highlighted in red
colour. The second sheet includes information about:

1.  Official symbol of a gene

2.  Official name of a gene

3.  Location of a gene

4.  DNA strand location of a gene

5.  Processes of a gene

6.  Components of a gene

7.  Functions of a gene

<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155212427-7125e1f4-2b79-4b48-b14e-34d7851432d6.png">

  Figure 2
</div>

## Information in the third sheet

The third sheet provides calculated values for further statistical
evaluation. The first row of the table is dedicated to calculations of
neighbouring genes their concordances (a) and disconcordances (b) of
processes, components and functions. The second row is dedicated to
calculations of a control group of randomly selected genes and it is
sorted at the same way as the first row, concordances (c) and
disconcordances (d). For better understanding a explanatory table is
given in figure 3.

<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155212573-db1774a8-89b5-4edd-b4f9-8f1b38830f30.png">
  Figure 3
</div>

<div align="center">
  <br>
  Table 2: An overview of the distribution of the acquired data in the third sheet
  <img src="https://user-images.githubusercontent.com/96398296/155212811-252c34da-29fb-46fd-812b-d93fab0297ee.png">
</div>

# Description of CWD Convertor

The convertor is a simple Windows Form application (see Figure 4). A
majority of the window is formed by a textbox that serves as the source
of information about managed tasks by the convertor. On the left from
the textbox there are six buttons divided into two sections. The buttons
in the first section are intended for loading above mentioned necessary
files. The second section is represented by a button for choosing a
folder for saving created reports. Selecting a folder automatically
triggers the process of creating reports for all genes given in
above-mentioned file.

<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155213121-c2ccd2af-a3f6-4553-988a-fc5c3d8d72c8.png">

  Figure 4: Main window of CWD Convertor
</div>

## Using the convertor

The principle of using this software is simple and intuitive, the
buttons are always used from the top to the bottom. The converter guides
the user by allowing to load the appropriate files (according to the
file extension).

At first it is important to download and prepare all necessary files.
GOA (.gaf), OBO and Reference genome of chosen organism in the form of a
feature table (.txt) are available to download (links are given in
chapter 5). Files containing genes in control group and genes for
comparison (.csv) have to be prepared manually (see appendix 1). Using
the converter is as follows:

1.  Click the button "Load GOA". A dialogue window appears to let the
    user select a .gaf file.
    
<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155213191-a691d147-57e1-4e03-9ce9-47bfe07fd8a1.png">
  
  Figure 5
</div>

2.  Click the button "Load Strands". A dialogue window appears to let
    the user select Genome Reference Consortium Human .txt file.

<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155213200-30b7297f-e8a2-4628-9b36-41f5f0187591.png">

  Figure 6
</div>

3.  Click the button "Open OBO". A dialogue window appears to let the
    user select the .obo file.

<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155213207-c95f6c19-c249-4b6c-8c5c-2418d1dad2a4.png">

  Figure 7
</div>

4.  Click the button "Load genes to compare". A dialogue window appears
    to let the user select a file containing official symbols of random
    genes in a .csv file.

<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155213215-637b36a6-917d-457d-bd70-c395d711fe57.png">

  Figure 8
</div>

5.  Click the button "Load Gene Family". A dialogue window appears to
    let the user select a file containing official symbols of genes for
    comparison in a .csv file.

<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155213228-fa073b99-48f2-40c7-8291-c55f705e2bab.png">

  Figure 9
</div>

6.  Click the button "Save to folder". A dialogue window appears, which
    allows user to choose a folder for saving created reports. CWD will
    inform user about a created report (.xlsx format) in textbox.

<div align="center">
  <img src="https://user-images.githubusercontent.com/96398296/155213239-7d3ce41c-c564-4551-a3fd-a8d80e3cf8df.png">

  Figure 10
</div>

It is possible for the user to continue with another flat file. It is
not necessary to load GOA, Genome Reference Consortium Human, OBO and
list of randomly selected genes again.

# Sources

**[Gene Ontology Annotation (GOA)]{.underline}**

[http://current.geneontology.org/products/pages/downloads.html]{.underline}

**[Open Biomedical Ontologies (OBO)]{.underline}**

[http://geneontology.org/docs/download-ontology/]{.underline}

**[Genome Reference Consortium Human (GRCh38.p13)]{.underline}**

<https://www.ncbi.nlm.nih.gov/assembly/GCF_000001405.39/>

# Appendix 1 -- Creating of a list of randomly selected genes

File containing genes in control group is prepared manually as follows.
Using spreadsheet editor, the user writes official symbols of genes in
the first column below each other. Such file is then saved as a csv
file.
