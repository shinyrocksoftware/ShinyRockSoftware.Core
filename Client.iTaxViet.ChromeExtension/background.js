let currentDownloadFilename = '';
const siteUrl = /https:\/\/bocaodientu.dkkd.gov.vn\/egazette\/Forms\/Egazette\/DefaultAnnouncements.aspx/;

const onTabChange = (tabId, isRightSite = false) => {
    if (isRightSite) {
        chrome.action.setBadgeBackgroundColor({color: [255, 0, 0, 255]});
    }

    chrome.action.setBadgeText({
        text: isRightSite ? 'on' : '',
        tabId
    });
};

const detectForIconChange = (url, tabId) => {
    let isRightSite = false;

    if (url && url.match(siteUrl)) {
        isRightSite = true;
    }

    onTabChange(tabId, isRightSite);

    return isRightSite;
};

const execute = (tabId) => {
    chrome.scripting.executeScript({
        target: {tabId: tabId},
        files: ['jquery-3.6.0.min.js', 'content.js']
    });
};

chrome.tabs.onActivated.addListener((info) => {
    chrome.tabs.get(info.tabId, change => {
        const isRightSite = detectForIconChange(change.url, info.tabId);
        if (isRightSite) {
            execute(info.tabId);
        }
    });
});

chrome.tabs.onUpdated.addListener((tabId, change, tab) => {
    const isRightSite = detectForIconChange(tab.url, tabId);
    if (isRightSite) {
        execute(tabId);
    }
});

chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
    if (request.name) {
        currentDownloadFilename = request.name;
        sendResponse(true);
    }
});

chrome.downloads.onDeterminingFilename.addListener((downloadItem, suggest) => {
    suggest({
        filename: currentDownloadFilename
    });

    // Send message to content script to notify file is downloaded
    chrome.tabs.query({active: true, currentWindow: true}, (tabs) => {
        chrome.tabs.sendMessage(tabs[0].id, {name: currentDownloadFilename});
    });
});