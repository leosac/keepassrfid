# KeePassRFID
KeePass 2.x RFID / NFC plugin to use your contactless card as a new key provider.

## Features
 * PC/SC, LibNFC and more reader protocol support: **implemented**
 * Card Serial Number (CSN) as a key: **implemented**
 * Key storage on NFC Tag (Type 4 only): **implemented**
 * Secure ID (3-way authentication, SAM, HSM): *planned* [*please vote for feature support here*](https://github.com/islog/keepassrfid/issues/5)
 
## System Requirements
 * [KeePass 2.18](http://keepass.info) or higher
 * Windows XP SP3 or higher
 * RFID Middleware [LibLogicalAccess SWIG](https://github.com/islog/liblogicalaccess-swig/releases)
 
## Hardware Requirements
WARNING: you **need** a reader to communicate with your contactless card! Only few computers have integrated contactless reader by default. If you don't know, you probably don't have one.

Most RFID / NFC readers and chips on the market are supported through the open-source LibLogicalAccess RFID/NFC middleware. See [Supported Hardware](https://github.com/islog/liblogicalaccess/wiki/Supported-hardware).
