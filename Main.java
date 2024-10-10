import java.io.*;
import java.util.Scanner;
import java.util.zip.ZipEntry;
import java.util.zip.ZipInputStream;
import java.util.zip.ZipOutputStream;

public class Main {

    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);

        System.out.println("Введите команду (pack/unpack):");
        String command = scanner.nextLine();

        System.out.println("Введите имя zip-архива:");
        String zipFileName = scanner.nextLine();

        System.out.println("Введите каталог/файл:");
        String sourceFile = scanner.nextLine();

        try {
            if ("pack".equalsIgnoreCase(command)) {
                pack(zipFileName, sourceFile);
            } else if ("unpack".equalsIgnoreCase(command)) {
                unpack(zipFileName, sourceFile);
            } else {
                System.out.println("Неизвестная команда: " + command);
            }
        } catch (IOException e) {
            System.err.println("Ошибка: " + e.getMessage());
        } finally {
            scanner.close();
        }
    }

    private static void pack(String zipFileName, String sourceFile) throws IOException {
        try (ZipOutputStream zos = new ZipOutputStream(new FileOutputStream(zipFileName))) {
            File fileToZip = new File(sourceFile);
            if (fileToZip.isDirectory()) {
                zipDirectory(fileToZip, fileToZip.getName(), zos);
            } else {
                zipFile(fileToZip, zos);
            }
            System.out.println("Архив " + zipFileName + " успешно создан.");
        }
    }

    private static void zipDirectory(File folderToZip, String folderName, ZipOutputStream zos) throws IOException {
        for (File file : folderToZip.listFiles()) {
            if (file.isDirectory()) {
                zipDirectory(file, folderName + "/" + file.getName(), zos);
            } else {
                zipFile(file, zos, folderName);
            }
        }
    }

    private static void zipFile(File fileToZip, ZipOutputStream zos) throws IOException {
        zipFile(fileToZip, zos, "");
    }

    private static void zipFile(File fileToZip, ZipOutputStream zos, String folderName) throws IOException {
        try (FileInputStream fis = new FileInputStream(fileToZip)) {
            ZipEntry zipEntry = new ZipEntry(folderName + (folderName.isEmpty() ? "" : "/") + fileToZip.getName());
            zos.putNextEntry(zipEntry);

            byte[] bytes = new byte[1024];
            int length;
            while ((length = fis.read(bytes)) >= 0) {
                zos.write(bytes, 0, length);
            }
            zos.closeEntry();
        }
    }

    private static void unpack(String zipFileName, String destDir) throws IOException {
        File dir = new File(destDir);
        if (!dir.exists()) {
            dir.mkdirs();
        }

        try (ZipInputStream zis = new ZipInputStream(new FileInputStream(zipFileName))) {
            ZipEntry zipEntry;
            while ((zipEntry = zis.getNextEntry()) != null) {
                File newFile = new File(destDir, zipEntry.getName());
                if (zipEntry.isDirectory()) {
                    newFile.mkdirs();
                } else {
                    // Создаем все необходимые родительские директории
                    new File(newFile.getParent()).mkdirs();
                    // Записываем файл
                    try (BufferedOutputStream bos = new BufferedOutputStream(new FileOutputStream(newFile))) {
                        byte[] buffer = new byte[1024];
                        int len;
                        while ((len = zis.read(buffer)) > 0) {
                            bos.write(buffer, 0, len);
                        }
                    }
                }
                zis.closeEntry();
            }
        }
        System.out.println("Архив " + zipFileName + " успешно распакован в " + destDir);
    }
}
