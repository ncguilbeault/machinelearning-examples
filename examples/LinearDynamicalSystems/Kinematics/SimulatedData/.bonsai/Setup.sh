#! /bin/bash

if [ ! -f "./Bonsai.exe" ]; then
    CONFIG="./Bonsai.config"
    if [ -f "$CONFIG" ]; then
        VERSION=$(xmllint --xpath '//PackageConfiguration/Packages/Package[@id="Bonsai"]/@version' "$CONFIG" | sed -e 's/^[^"]*"//' -e 's/"$//')
        RELEASE="https://github.com/bonsai-rx/bonsai/releases/download/$VERSION/Bonsai.zip"
    else
        RELEASE="https://github.com/bonsai-rx/bonsai/releases/latest/download/Bonsai.zip"
    fi
    wget $RELEASE -O "temp.zip"
    mv -f "NuGet.config" "temp.config"
    unzip -d "." -o "temp.zip"
    mv -f "temp.config" "NuGet.config"
    rm -rf "temp.zip"
    rm -rf "Bonsai32.exe"
fi

if [ -f "./Bonsai.config" ]; then
    ASSEMBLYLOCATIONS=$(xmllint --xpath '//PackageConfiguration/AssemblyLocations/AssemblyLocation/@location' "$CONFIG" | sed -e 's/^[^"]*"//' -e 's/"$//')
    for ASSEMBLYLOCATION in $ASSEMBLYLOCATIONS;
    do
        NEWASSEMBLYLOCATION="${ASSEMBLYLOCATION//\\/\/}"
        xmlstarlet edit --inplace --update "/PackageConfiguration/AssemblyLocations/AssemblyLocation[@location='$ASSEMBLYLOCATION']/@location" --value "$NEWASSEMBLYLOCATION" "$CONFIG"
    done

    LIBRARYFOLDERS=$(xmllint --xpath '//PackageConfiguration/LibraryFolders/LibraryFolder/@path' "$CONFIG" | sed -e 's/^[^"]*"//' -e 's/"$//')
    for LIBRARYFOLDER in $LIBRARYFOLDERS;
    do
        NEWLIBRARYFOLDER="${LIBRARYFOLDER//\\/\/}"
        xmlstarlet edit --inplace --update "//PackageConfiguration/LibraryFolders/LibraryFolder[@path='$LIBRARYFOLDER']/@path" --value "$NEWLIBRARYFOLDER" "$CONFIG"
    done
fi

mono Bonsai.exe --no-editor