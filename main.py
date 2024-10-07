import cv2
import numpy as np
import scipy
import math


def main():
    print("main()")
    ddepth = cv2.CV_16S
    kernel_size = 3
    image = cv2.imread("carbon.jpg", 0)
    print("image", image)
    fourier = scipy.fft.fft2(image)
    real_part = np.real(fourier)
    imag_part = np.imag(fourier)
    sum_real_imag = np.power(real_part, 2) + np.power(imag_part, 2)
    fuck = np.sqrt(sum_real_imag)
    print("fuck", fuck)
    fuck2 = np.log2(fuck + 1)
    print("fuck2", fuck2)

    t_min = np.min(fuck2)
    t_max = np.max(fuck2)

    prikol = ((fuck2 - t_min)/(t_max - t_min)) * 255 + 0.5
    print("prikol", prikol)
    prikol2 = np.floor(prikol)
    print("prikol2", prikol2)

    fourier2 = scipy.fft.ifft(prikol2)
    cock = np.real(fourier2)
    res = cv2.resize(cock, None, fx=0.5, fy=0.5)
    cv2.imshow('res', res)
    cv2.waitKey(0)
    cv2.imwrite("carbon_fourier.jpg", res)
    # image_blur = cv2.GaussianBlur(image, (3, 3), 0)
    # # dst = cv2.Laplacian(image_blur, ddepth, ksize=kernel_size)
    # # res = cv2.convertScaleAbs(dst)
    # ret2, res = cv2.threshold(image_blur, 0, 255, cv2.THRESH_BINARY + cv2.THRESH_OTSU)
    # # res = cv2.Canny(image_blur, 50, 200, None, 3)
    # cdst = cv2.cvtColor(res, cv2.COLOR_GRAY2BGR)
    # cdstP = np.copy(cdst)
    #
    # lines = cv2.HoughLines(res, 1, np.pi / 180, 150, None, 0, 0)
    #
    # if lines is not None:
    #     for i in range(0, len(lines)):
    #         rho = lines[i][0][0]
    #         theta = lines[i][0][1]
    #         a = math.cos(theta)
    #         b = math.sin(theta)
    #         x0 = a * rho
    #         y0 = b * rho
    #         pt1 = (int(x0 + 1000 * (-b)), int(y0 + 1000 * (a)))
    #         pt2 = (int(x0 - 1000 * (-b)), int(y0 - 1000 * (a)))
    #         cv2.line(cdst, pt1, pt2, (0, 0, 255), 3, cv2.LINE_AA)
    #
    # linesP = cv2.HoughLinesP(res, 1, np.pi / 180, 50, None, 50, 10)
    #
    # if linesP is not None:
    #     for i in range(0, len(linesP)):
    #         l = linesP[i][0]
    #         cv2.line(cdstP, (l[0], l[1]), (l[2], l[3]), (0, 0, 255), 3, cv2.LINE_AA)
    #
    # cv2.imshow("Source", image)
    # cv2.imshow("Detected Lines (in red) - Standard Hough Line Transform", cdst)
    # cv2.imshow("Detected Lines (in red) - Probabilistic Line Transform", cdstP)

    # res = cv2.adaptiveThreshold(res, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY, 11, 2)

    # fourier = scipy.fft.dst(image)

    # fourier = scipy.fft.ifft2(fourier)
    # res = np.real(fourier)
    # result = color_quantization(img)
    # print(result)
    # res = cv2.resize(res, None, fx=0.5, fy=0.5)
    # cv2.imshow('res', res)
    # cv2.waitKey(0)
    # cv2.imwrite("carbon_fourier.jpg", res)


def color_quantization(img):
    Z = img.reshape((-1, 3))
    # convert to np.float32
    Z = np.float32(Z)
    # define criteria, number of clusters(K) and apply kmeans()
    criteria = (cv2.TERM_CRITERIA_EPS + cv2.TERM_CRITERIA_MAX_ITER, 10, 1.0)
    K = 2
    ret, label, center = cv2.kmeans(Z, K, None, criteria, 10, cv2.KMEANS_RANDOM_CENTERS)
    # Now convert back into uint8, and make original image
    center = np.uint8(center)
    res = center[label.flatten()]
    res2 = res.reshape((img.shape))
    return res2

if __name__ == "__main__":
    main()