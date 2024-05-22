{
    let storageKey = 'numbers';
    let numbers = [];
    let currentDownloadFilename = '';

    let testData = ['0109902503', '0109902493', '0109902422'];

    const setStorage = (key, value, callback) => {
        const val = {};
        val[key] = value;

        chrome.storage.local.set(val, callback);
    };

    const getStorage = (key, callback) => {
        chrome.storage.local.get([key], callback);
    };

    setInterval(() => {
        if (!currentDownloadFilename) {
            setTimeout(() => {
                const existedTaxIdentificationNumbers = testData;

                $('table.gridview tbody tr').each((trI, trE) => {
                    // Exclude header and pager
                    if (trI > 0 && trI < $('table.gridview tbody tr').length - 2) {
                        const company = {};
                        let fileE = null;

                        $(trE).find('td').each((tdI, tdE) => {
                            const content = $(tdE).text().trim();

                            if (tdI === 0) {
                                company.establishmentAt = content;
                            }

                            if (tdI === 1) {
                                const names = content.replace(/\s+/g, ' ');
                                const taxIdentificationNumberStr = 'MÃ SỐ DN: ';
                                const taxIdentificationNumberIndex = names.indexOf(taxIdentificationNumberStr);

                                company.name = names.slice(0, taxIdentificationNumberIndex - 1);
                                company.taxIdentificationNumber = names.slice(taxIdentificationNumberIndex + taxIdentificationNumberStr.length);
                            }

                            if (tdI === 2) {
                                company.city = content;
                            }

                            if (tdI === 3) {
                                company.type = content;
                            }

                            if (tdI === 4) {
                                fileE = $(tdE).find('input');
                                company.file = content;
                            }
                        });

                        if (existedTaxIdentificationNumbers.indexOf(company.taxIdentificationNumber) === -1) {
                            currentDownloadFilename = `${company.taxIdentificationNumber}.pdf`;
                            console.log(currentDownloadFilename);

                            // Send tax identification number to background script to set the download file name
                            chrome.runtime.sendMessage({
                                name: currentDownloadFilename
                            }, () => {
                                // Start download file
                                fileE.trigger('click');

                                return false;
                            });

                            return false;
                        }
                    }
                });
            }, 5000);
        }
    }, 5000);

    chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
        if (request.name === currentDownloadFilename) {
            currentDownloadFilename = '';
        }
    });
}