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

  -----------------------------------------------------------------------
  Colour              Meaning
  ------------------- ---------------------------------------------------
                      Value in this cell is a process of a gene

                      Value in this cell is a component of a gene

                      Value in this cell is a function of a gene
  -----------------------------------------------------------------------

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

![](media/image1.png){width="6.2472200349956255in" height="3.78125in"}

Figure 1

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

![](media/image2.png){width="6.29166447944007in"
height="3.7291666666666665in"}

Figure 2

## Information in the third sheet

The third sheet provides calculated values for further statistical
evaluation. The first row of the table is dedicated to calculations of
neighbouring genes their concordances (a) and disconcordances (b) of
processes, components and functions. The second row is dedicated to
calculations of a control group of randomly selected genes and it is
sorted at the same way as the first row, concordances (c) and
disconcordances (d). For better understanding a explanatory table is
given in figure 3.

![](media/image3.png){width="5.0513899825021875in"
height="0.7395833333333334in"}

Figure 3

  ---------------------------------------------------------------------------------------
                                       Process         Component         Function   
  ------------------------------------ --------- ----- ----------- ----- ---------- -----
  Official symbol of the main gene     a         b     a           b     a          b

                                       c         d     c           d     c          d
  ---------------------------------------------------------------------------------------

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

![](media/image4.png){width="3.5506135170603677in"
height="3.408588145231846in"}

Figure 4: Main window of CWD Convertor

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

![](media/image5.emf){width="6.3in" height="3.5194444444444444in"}

Figure 5

2.  Click the button "Load Strands". A dialogue window appears to let
    the user select Genome Reference Consortium Human .txt file.

![](media/image6.emf){width="6.3in" height="3.5194444444444444in"}

Figure 6

3.  Click the button "Open OBO". A dialogue window appears to let the
    user select the .obo file.

![](media/image7.emf){width="6.3in" height="3.5194444444444444in"}

Figure 7

4.  Click the button "Load genes to compare". A dialogue window appears
    to let the user select a file containing official symbols of random
    genes in a .csv file.

![](media/image8.emf){width="6.3in" height="3.5194444444444444in"}Figure
8

5.  Click the button "Load Gene Family". A dialogue window appears to
    let the user select a file containing official symbols of genes for
    comparison in a .csv file.

![](media/image9.emf){width="6.3in" height="3.5194444444444444in"}

Figure 9

6.  Click the button "Save to folder". A dialogue window appears, which
    allows user to choose a folder for saving created reports. CWD will
    inform user about a created report (.xlsx format) in textbox.

> ![](media/image10.emf){width="6.3in" height="3.5194444444444444in"}

Figure 10

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

# Appendix 1 -- Creating of a list of randomly selected genes {#appendix-1-creating-of-a-list-of-randomly-selected-genes .NAzev-appendicx}

File containing genes in control group is prepared manually as follows.
Using spreadsheet editor, the user writes official symbols of genes in
the first column below each other. Such file is then saved as a csv
file.
