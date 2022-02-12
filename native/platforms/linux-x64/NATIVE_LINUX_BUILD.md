# Build native dependencies for Linux platforms 

# Introduction
  **Novacta.Analytics** relies on 
  native dynamic-link libraries obtained
  via the Intel® oneAPI Math Kernel Library customDLL builder.
  [oneAPI MKL](https://www.intel.com/content/www/us/en/developer/tools/oneapi/onemkl.html) 
  is Copyright (c) 2021 Intel® Corporation.

  On Linux systems, **Novacta.Analytics** depends on a custom native library 
  named `libna.so`, which in turn has a dependency on the redistributable 
  `libiomp5.so` library. The following instructions enable the inclusion of
  these objects in the **Novacta.Analytics** project, by storing them in 
  its sub-folder
  ```
  /runtimes/linux-x64/native/
  ```

  In what follows, it is assumed that the Linux Intel® oneAPI BaseKit, 
  version 2022.1.2.146, has been installed in its default directory. 
  Otherwise, paths should be modified according to the location of the 
  currently installed version.
 
# How to build
 1. Set variable `LIBNAROOT` to the directory where this repo has been cloned
    on your Linux machine, by executing a command like the following:
    ```
    export LIBNAROOT=/Users/user/Source/Repos/analytics
    ```
 2. Build the `libna.so` native dependency:
    ```
    sudo make intel64 -C /opt/intel/oneapi/mkl/2022.0.2/tools/builder export=$LIBNAROOT/native/exports/mkl_exports_linux.txt name=$LIBNAROOT/src/Novacta.Analytics/runtimes/linux-x64/native/libna
    ```
 3. Copy the required redistributable `libiomp5.so`:
    ```
    cp /opt/intel/oneapi/compiler/2022.0.2/linux/compiler/lib/intel64/libiomp5.so $LIBNAROOT/src/Novacta.Analytics/runtimes/linux-x64/native
    ```