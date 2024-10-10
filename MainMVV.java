package org.example;

import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.Files;
import java.util.zip.*;
import java.io.*;

public class Main {
    public static void main(String[] args) throws IOException {
        if (args[0].equals("pack")) {
            pack2(args[1], args[2]);
        } else if (args[0].equals("unpack")) {
            unpack(args[1], args[2]);
        }
        else {
            System.out.println("Unknown Command...");
        }
    }

    private static void pack(/*String zipName, String fileToZip*/) throws IOException {
        byte[] buffer = Files.readAllBytes(Paths.get("d:/notes.txt"));
        try (ZipOutputStream zout = new ZipOutputStream(new FileOutputStream("d:/outputZipchik.zip"))) {
            ZipEntry entry1 = new ZipEntry("zipchik");
            zout.putNextEntry(entry1);
            zout.write(buffer);
            zout.closeEntry();
        }
    }


    private static void pack2(String zipFilePath, String sourcePath) throws IOException{
        Path p = Files.createFile(Paths.get(zipFilePath));
        try (ZipOutputStream zs = new ZipOutputStream(Files.newOutputStream(p))) {
            Path pp = Paths.get(sourcePath);
            Files.walk(pp).filter(path -> !Files.isDirectory(path)).forEach(path -> {
                ZipEntry zipEntry = new ZipEntry(pp.relativize(path).toString());
                try {
                    zs.putNextEntry(zipEntry);
                    Files.copy(path, zs);
                    zs.closeEntry();
                } catch (IOException e) {
                    System.err.println(e);
                }
            });
        }
    }

    private static void unpack(String zipFilePath, String outputPath) throws IOException {
        File base = new File(outputPath);
        base.mkdir();
        try (ZipInputStream zin = new ZipInputStream(new FileInputStream(zipFilePath))) {
            ZipEntry entry;
            while ((entry = zin.getNextEntry()) != null) {
                System.out.println(entry.getName());
                if (entry.getName().endsWith("/")) {
                    base = new File(outputPath + "/" + entry.getName());
                    base.mkdir();
                    base = new File(outputPath);
                }
                else {
                    try (FileOutputStream fout = new FileOutputStream(new File(base, entry.getName()))) {

                        for (int c = zin.read(); c != -1; c = zin.read()) {
                            fout.write(c);
                        }
                    }
                    zin.closeEntry();
                }

            }
        }
    }
}