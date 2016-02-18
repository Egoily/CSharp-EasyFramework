
    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK11E386F5918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_BANK'))
alter table CRM_CUSTOMERS_BANK  drop constraint FK11E386F5918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2804FD15EED2B984]') AND parent_object_id = OBJECT_ID('CRM_MVNO_BRS_TAXCODE_POSTCODE'))
alter table CRM_MVNO_BRS_TAXCODE_POSTCODE  drop constraint FK2804FD15EED2B984


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8A4264688CAF124E]') AND parent_object_id = OBJECT_ID('RM_BUNDLE_PACKAGE'))
alter table RM_BUNDLE_PACKAGE  drop constraint FK8A4264688CAF124E


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1798439F9B7C8FC]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_PROMOTIONGROUPS'))
alter table CRM_CUSTOMERS_PROMOTIONGROUPS  drop constraint FK1798439F9B7C8FC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1798439F918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_PROMOTIONGROUPS'))
alter table CRM_CUSTOMERS_PROMOTIONGROUPS  drop constraint FK1798439F918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7D536073918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_PROMOTION'))
alter table CRM_CUSTOMERS_PROMOTION  drop constraint FK7D536073918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7D53607381DC76C9]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_PROMOTION'))
alter table CRM_CUSTOMERS_PROMOTION  drop constraint FK7D53607381DC76C9


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9BC81200918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_RESOURCEMB'))
alter table CRM_CUSTOMERS_RESOURCEMB  drop constraint FK9BC81200918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9BC81200240E3AD]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_RESOURCEMB'))
alter table CRM_CUSTOMERS_RESOURCEMB  drop constraint FK9BC81200240E3AD


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK482EEC20C3631F53]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_RESOURCEMB_PROPERTY'))
alter table CRM_CUSTOMERS_RESOURCEMB_PROPERTY  drop constraint FK482EEC20C3631F53


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2DFA0D858E62C593]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_UNPAIDFEE'))
alter table CRM_CUSTOMERS_UNPAIDFEE  drop constraint FK2DFA0D858E62C593


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD9B256F4918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_CREDITCARDS'))
alter table CRM_CUSTOMERS_CREDITCARDS  drop constraint FKD9B256F4918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB9AF7D7D918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_DATA_ROAMING_LIMIT'))
alter table CRM_CUSTOMERS_DATA_ROAMING_LIMIT  drop constraint FKB9AF7D7D918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC98644EA918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_DATA_ROAMING_LIMIT_NOTIFICATION'))
alter table CRM_CUSTOMERS_DATA_ROAMING_LIMIT_NOTIFICATION  drop constraint FKC98644EA918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC98644EA62CB716B]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_DATA_ROAMING_LIMIT_NOTIFICATION'))
alter table CRM_CUSTOMERS_DATA_ROAMING_LIMIT_NOTIFICATION  drop constraint FKC98644EA62CB716B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKAE0C9D6A1B2C3B3]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_ADDRESSES'))
alter table CRM_CUSTOMERS_ADDRESSES  drop constraint FKAE0C9D6A1B2C3B3


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKAE0C9D6A918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_ADDRESSES'))
alter table CRM_CUSTOMERS_ADDRESSES  drop constraint FKAE0C9D6A918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK72F57464E681CE1B]') AND parent_object_id = OBJECT_ID('CRM_MVNO_TOPUP_CONFIG'))
alter table CRM_MVNO_TOPUP_CONFIG  drop constraint FK72F57464E681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF1C3133EE681CE1B]') AND parent_object_id = OBJECT_ID('CRM_DEALERS_BANK'))
alter table CRM_DEALERS_BANK  drop constraint FKF1C3133EE681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC95970FCE681CE1B]') AND parent_object_id = OBJECT_ID('CRM_DEALERS_LOYALTY_POINT'))
alter table CRM_DEALERS_LOYALTY_POINT  drop constraint FKC95970FCE681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK38A05257E681CE1B]') AND parent_object_id = OBJECT_ID('CRM_DEALERS_PROPERTIES'))
alter table CRM_DEALERS_PROPERTIES  drop constraint FK38A05257E681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK71B39DC56D277E12]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_DID'))
alter table CRM_CUSTOMERS_DID  drop constraint FK71B39DC56D277E12


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKD3E349DD83C86550]') AND parent_object_id = OBJECT_ID('CRM_EMAIL_ATTACHMENT_HISTORY'))
alter table CRM_EMAIL_ATTACHMENT_HISTORY  drop constraint FKD3E349DD83C86550


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKFFD9BC93A6340610]') AND parent_object_id = OBJECT_ID('RM_INTERNATIONAL_ZONE_DETAILS'))
alter table RM_INTERNATIONAL_ZONE_DETAILS  drop constraint FKFFD9BC93A6340610


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK961F21D8EDCB41EE]') AND parent_object_id = OBJECT_ID('MVNO_FC_LIFECYCLE_LOG_EXTEND'))
alter table MVNO_FC_LIFECYCLE_LOG_EXTEND  drop constraint FK961F21D8EDCB41EE


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK47B4C79A918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_MAPPING'))
alter table CRM_CUSTOMERS_MAPPING  drop constraint FK47B4C79A918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9B65EF3BE681CE1B]') AND parent_object_id = OBJECT_ID('CRM_MVNO_CONFIGACTION'))
alter table CRM_MVNO_CONFIGACTION  drop constraint FK9B65EF3BE681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE188EF2F918EF537]') AND parent_object_id = OBJECT_ID('CRM_MVNO_CUSTOMERS_PROPERTY'))
alter table CRM_MVNO_CUSTOMERS_PROPERTY  drop constraint FKE188EF2F918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDB9DEDCADD7BC768]') AND parent_object_id = OBJECT_ID('CRM_MVNO_DATA_ROAMING_LIMIT'))
alter table CRM_MVNO_DATA_ROAMING_LIMIT  drop constraint FKDB9DEDCADD7BC768


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK24095A4DD7BC768]') AND parent_object_id = OBJECT_ID('CRM_MVNO_DATA_ROAMING_LIMIT_NOTIFICATION'))
alter table CRM_MVNO_DATA_ROAMING_LIMIT_NOTIFICATION  drop constraint FK24095A4DD7BC768


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK39AD6064E681CE1B]') AND parent_object_id = OBJECT_ID('CRM_MVNO_PROPERTIES'))
alter table CRM_MVNO_PROPERTIES  drop constraint FK39AD6064E681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKB12B7BCD7E5EB055]') AND parent_object_id = OBJECT_ID('CRM_MVNO_OPERATION_DETAILS_LOG'))
alter table CRM_MVNO_OPERATION_DETAILS_LOG  drop constraint FKB12B7BCD7E5EB055


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5758805C7BDE710E]') AND parent_object_id = OBJECT_ID('CRM_MVNO_OPERATION_LOG'))
alter table CRM_MVNO_OPERATION_LOG  drop constraint FK5758805C7BDE710E


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5758805C1624A2CC]') AND parent_object_id = OBJECT_ID('CRM_MVNO_OPERATION_LOG'))
alter table CRM_MVNO_OPERATION_LOG  drop constraint FK5758805C1624A2CC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8D729B2BB67DA44]') AND parent_object_id = OBJECT_ID('RM_PACKAGE_RELATIONSHIPS'))
alter table RM_PACKAGE_RELATIONSHIPS  drop constraint FK8D729B2BB67DA44


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8D729B2B76B64A25]') AND parent_object_id = OBJECT_ID('RM_PACKAGE_RELATIONSHIPS'))
alter table RM_PACKAGE_RELATIONSHIPS  drop constraint FK8D729B2B76B64A25


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA4BE07FF918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_PRODUCTS'))
alter table CRM_CUSTOMERS_PRODUCTS  drop constraint FKA4BE07FF918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA4BE07FF8CAF124E]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_PRODUCTS'))
alter table CRM_CUSTOMERS_PRODUCTS  drop constraint FKA4BE07FF8CAF124E


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA6B3BE4F918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_PROPERTY'))
alter table CRM_CUSTOMERS_PROPERTY  drop constraint FKA6B3BE4F918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE018525D9B7C8FC]') AND parent_object_id = OBJECT_ID('RM_PROMOTIONGROUP_MEMBERS'))
alter table RM_PROMOTIONGROUP_MEMBERS  drop constraint FKE018525D9B7C8FC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE018525D8E62C593]') AND parent_object_id = OBJECT_ID('RM_PROMOTIONGROUP_MEMBERS'))
alter table RM_PROMOTIONGROUP_MEMBERS  drop constraint FKE018525D8E62C593


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7C5260A88E62C593]') AND parent_object_id = OBJECT_ID('RM_PROMOTIONPLAN_DETAIL'))
alter table RM_PROMOTIONPLAN_DETAIL  drop constraint FK7C5260A88E62C593


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1AE8C8498E62C593]') AND parent_object_id = OBJECT_ID('RM_PROMOTIONPLAN_RULE'))
alter table RM_PROMOTIONPLAN_RULE  drop constraint FK1AE8C8498E62C593


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK245B0A718E62C593]') AND parent_object_id = OBJECT_ID('RM_SPECIFICNUMBER_GROUP'))
alter table RM_SPECIFICNUMBER_GROUP  drop constraint FK245B0A718E62C593


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKDADDF87E681CE1B]') AND parent_object_id = OBJECT_ID('CRM_ROAMING_SETTING'))
alter table CRM_ROAMING_SETTING  drop constraint FKDADDF87E681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK42540FC72CC1C7F0]') AND parent_object_id = OBJECT_ID('RM_ROAMING_ZONE_DETAILS'))
alter table RM_ROAMING_ZONE_DETAILS  drop constraint FK42540FC72CC1C7F0


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKAD691CA4672541E2]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_SERVICES'))
alter table CRM_CUSTOMERS_SERVICES  drop constraint FKAD691CA4672541E2


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKAD691CA4918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_SERVICES'))
alter table CRM_CUSTOMERS_SERVICES  drop constraint FKAD691CA4918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKAD691CA4467CA9B]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_SERVICES'))
alter table CRM_CUSTOMERS_SERVICES  drop constraint FKAD691CA4467CA9B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK94A90C99DC34746B]') AND parent_object_id = OBJECT_ID('CRM_LIFECYCLE_SETTINGS_DETAILS'))
alter table CRM_LIFECYCLE_SETTINGS_DETAILS  drop constraint FK94A90C99DC34746B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9230C4A4E5A56660]') AND parent_object_id = OBJECT_ID('CRM_LIFECYCLE_SETTINGS'))
alter table CRM_LIFECYCLE_SETTINGS  drop constraint FK9230C4A4E5A56660


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8C3923A3DC34746B]') AND parent_object_id = OBJECT_ID('CRM_LIFECYCLE_SMS_TEMPLETS'))
alter table CRM_LIFECYCLE_SMS_TEMPLETS  drop constraint FK8C3923A3DC34746B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1D084411B7AD2A0A]') AND parent_object_id = OBJECT_ID('SYS25_USER_DEALER'))
alter table SYS25_USER_DEALER  drop constraint FK1D084411B7AD2A0A


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK8962D8ADE681CE1B]') AND parent_object_id = OBJECT_ID('CRM_DEALERS_OBOPRS'))
alter table CRM_DEALERS_OBOPRS  drop constraint FK8962D8ADE681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK82A80B36C3631F53]') AND parent_object_id = OBJECT_ID('CRM_MOBILE_MULTIPLEIMSI'))
alter table CRM_MOBILE_MULTIPLEIMSI  drop constraint FK82A80B36C3631F53


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK2533C01FC3631F53]') AND parent_object_id = OBJECT_ID('CRM_MOBILE_NETWORK'))
alter table CRM_MOBILE_NETWORK  drop constraint FK2533C01FC3631F53


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7A5E7F7E8F47D972]') AND parent_object_id = OBJECT_ID('CRM_DEFAULT_PROVISION_NETWORK'))
alter table CRM_DEFAULT_PROVISION_NETWORK  drop constraint FK7A5E7F7E8F47D972


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK6A144CEBE681CE1B]') AND parent_object_id = OBJECT_ID('CRM_DEFAULT_PROVISION_ROAMING_SETTING'))
alter table CRM_DEFAULT_PROVISION_ROAMING_SETTING  drop constraint FK6A144CEBE681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK49611DE3918EF537]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_REMARKS'))
alter table CRM_CUSTOMERS_REMARKS  drop constraint FK49611DE3918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF3F9627F6D1EF1B1]') AND parent_object_id = OBJECT_ID('CRM_BUSINESSRULES_PROMOTIONGROUPS'))
alter table CRM_BUSINESSRULES_PROMOTIONGROUPS  drop constraint FKF3F9627F6D1EF1B1


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF3F9627FD67CDE98]') AND parent_object_id = OBJECT_ID('CRM_BUSINESSRULES_PROMOTIONGROUPS'))
alter table CRM_BUSINESSRULES_PROMOTIONGROUPS  drop constraint FKF3F9627FD67CDE98


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF3F9627F9B7C8FC]') AND parent_object_id = OBJECT_ID('CRM_BUSINESSRULES_PROMOTIONGROUPS'))
alter table CRM_BUSINESSRULES_PROMOTIONGROUPS  drop constraint FKF3F9627F9B7C8FC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_CRM_CUSTOMER_SERVICESID]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY'))
alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY  drop constraint FK_CRM_CUSTOMER_SERVICESID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_CRM_CUSTOMER_PROMOTIONSID]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY'))
alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY  drop constraint FK_CRM_CUSTOMER_PROMOTIONSID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_CRM_OPERATION_EXECUTIONSID]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY'))
alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY  drop constraint FK_CRM_OPERATION_EXECUTIONSID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_CRM_CUSTOMERSID]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY'))
alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY  drop constraint FK_CRM_CUSTOMERSID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_RM_PROMOTIONPLANID]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY'))
alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY  drop constraint FK_RM_PROMOTIONPLANID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_CRM_CUSTOMER_RESOURCEMBID]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY'))
alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY  drop constraint FK_CRM_CUSTOMER_RESOURCEMBID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_RM_PACKAGES]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY'))
alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY  drop constraint FK_RM_PACKAGES


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK514F7399C39BC787]') AND parent_object_id = OBJECT_ID('dbo.TEXT_LOOKUP_TRANSLATIONS'))
alter table dbo.TEXT_LOOKUP_TRANSLATIONS  drop constraint FK514F7399C39BC787


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1793C4226531341]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK1793C4226531341


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1793C42239883E47]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK1793C42239883E47


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_PARENT_OPERATIONID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_PARENT_OPERATIONID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ROOT_OPEXECUTIONID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_ROOT_OPEXECUTIONID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_USERID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_USERID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_DEALERID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_DEALERID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_CUSTOMERID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_CUSTOMERID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_DESTINATION_CUSTOMER_ID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_DESTINATION_CUSTOMER_ID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_RESOURCEMBID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_RESOURCEMBID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_DESTINATION_RESOURCEMBID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_DESTINATION_RESOURCEMBID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_SYS_NPMID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_SYS_NPMID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_ACCOUNTID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_ACCOUNTID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERATION_PRODUCTID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERATION_PRODUCTID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_CUST_PROD_ASSNID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_CUST_PROD_ASSNID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERAION_SIMID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERAION_SIMID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERATION_ORDER_ID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_EXECUTIONS'))
alter table CRM_OPERATION_EXECUTIONS  drop constraint FK_OPERATION_ORDER_ID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TEXT_LOOKUP]') AND parent_object_id = OBJECT_ID('CRM_OPERATIONS'))
alter table CRM_OPERATIONS  drop constraint FK_TEXT_LOOKUP


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_CONFIG_DEALER]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_CONFIGURATIONS'))
alter table CRM_OPERATION_CONFIGURATIONS  drop constraint FK_CONFIG_DEALER


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_OPERATIONID]') AND parent_object_id = OBJECT_ID('CRM_OPERATION_CONFIGURATIONS'))
alter table CRM_OPERATION_CONFIGURATIONS  drop constraint FK_OPERATIONID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ORDER_DEALERID]') AND parent_object_id = OBJECT_ID('CRM_ORDERS'))
alter table CRM_ORDERS  drop constraint FK_ORDER_DEALERID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_PARENT_ORDER_ID]') AND parent_object_id = OBJECT_ID('CRM_ORDERS'))
alter table CRM_ORDERS  drop constraint FK_PARENT_ORDER_ID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ORDER_OPERATION_ID]') AND parent_object_id = OBJECT_ID('CRM_ORDERS'))
alter table CRM_ORDERS  drop constraint FK_ORDER_OPERATION_ID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_ORDER_TRANSITION_ORDERID]') AND parent_object_id = OBJECT_ID('CRM_ORDER_STATUS_HISTORY'))
alter table CRM_ORDER_STATUS_HISTORY  drop constraint FK_ORDER_TRANSITION_ORDERID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK_TRANSITION_OPERATION_EXECUTIONID]') AND parent_object_id = OBJECT_ID('CRM_ORDER_STATUS_HISTORY'))
alter table CRM_ORDER_STATUS_HISTORY  drop constraint FK_TRANSITION_OPERATION_EXECUTIONID


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK78C2F32781C64F30]') AND parent_object_id = OBJECT_ID('RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS'))
alter table RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS  drop constraint FK78C2F32781C64F30


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK78C2F3272BB74448]') AND parent_object_id = OBJECT_ID('RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS'))
alter table RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS  drop constraint FK78C2F3272BB74448


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC8F35322A9D67797]') AND parent_object_id = OBJECT_ID('CRM_CUSTOMERS_PROMOTION_LIMITS'))
alter table CRM_CUSTOMERS_PROMOTION_LIMITS  drop constraint FKC8F35322A9D67797


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKE150D139B7C8FC]') AND parent_object_id = OBJECT_ID('dbo.RM_PROMOTIONGROUP_THRESHOLDS'))
alter table dbo.RM_PROMOTIONGROUP_THRESHOLDS  drop constraint FKE150D139B7C8FC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKBDC1122364773DF]') AND parent_object_id = OBJECT_ID('dbo.SYS_NPM_DEALER'))
alter table dbo.SYS_NPM_DEALER  drop constraint FKBDC1122364773DF


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK5645BBB064773DF]') AND parent_object_id = OBJECT_ID('dbo.SYS_NPM_PROPERTY'))
alter table dbo.SYS_NPM_PROPERTY  drop constraint FK5645BBB064773DF


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK76C3DB70E681CE1B]') AND parent_object_id = OBJECT_ID('CRM_MOBILE_SIMCARDS'))
alter table CRM_MOBILE_SIMCARDS  drop constraint FK76C3DB70E681CE1B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKEE721C555C0F5C8]') AND parent_object_id = OBJECT_ID('dbo.CRM_ACCOUNTS'))
alter table dbo.CRM_ACCOUNTS  drop constraint FKEE721C555C0F5C8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKEE721C553370A309]') AND parent_object_id = OBJECT_ID('dbo.CRM_ACCOUNTS'))
alter table dbo.CRM_ACCOUNTS  drop constraint FKEE721C553370A309


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKEE721C55B83192F2]') AND parent_object_id = OBJECT_ID('dbo.CRM_ACCOUNTS'))
alter table dbo.CRM_ACCOUNTS  drop constraint FKEE721C55B83192F2


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKEE721C557EBB0BF4]') AND parent_object_id = OBJECT_ID('dbo.CRM_ACCOUNTS'))
alter table dbo.CRM_ACCOUNTS  drop constraint FKEE721C557EBB0BF4


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKEE721C5585F3D114]') AND parent_object_id = OBJECT_ID('dbo.CRM_ACCOUNTS'))
alter table dbo.CRM_ACCOUNTS  drop constraint FKEE721C5585F3D114


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK4B14693C52514B24]') AND parent_object_id = OBJECT_ID('dbo.CRM_ACCOUNT_BALANCES'))
alter table dbo.CRM_ACCOUNT_BALANCES  drop constraint FK4B14693C52514B24


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK2DD05787DD7BC768]') AND parent_object_id = OBJECT_ID('dbo.RM_BILL_CYCLES'))
alter table dbo.RM_BILL_CYCLES  drop constraint FK2DD05787DD7BC768


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK2DD057873370A309]') AND parent_object_id = OBJECT_ID('dbo.RM_BILL_CYCLES'))
alter table dbo.RM_BILL_CYCLES  drop constraint FK2DD057873370A309


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK31B5AA38B83192F2]') AND parent_object_id = OBJECT_ID('dbo.RM_BILL_RUN'))
alter table dbo.RM_BILL_RUN  drop constraint FK31B5AA38B83192F2


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK25382FA05C0F5C8]') AND parent_object_id = OBJECT_ID('dbo.RM_CHARGES'))
alter table dbo.RM_CHARGES  drop constraint FK25382FA05C0F5C8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK25382FA03370A309]') AND parent_object_id = OBJECT_ID('dbo.RM_CHARGES'))
alter table dbo.RM_CHARGES  drop constraint FK25382FA03370A309


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK44A053914D45695C]') AND parent_object_id = OBJECT_ID('RM_CHARGE_OPTIONS'))
alter table RM_CHARGE_OPTIONS  drop constraint FK44A053914D45695C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK44A0539192203841]') AND parent_object_id = OBJECT_ID('RM_CHARGE_OPTIONS'))
alter table RM_CHARGE_OPTIONS  drop constraint FK44A0539192203841


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKF8C80A8B92203841]') AND parent_object_id = OBJECT_ID('dbo.RM_CHARGE_PRICES'))
alter table dbo.RM_CHARGE_PRICES  drop constraint FKF8C80A8B92203841


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKA2F2647192203841]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMER_CHARGE_SCHEDULES'))
alter table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES  drop constraint FKA2F2647192203841


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKA2F26471918EF537]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMER_CHARGE_SCHEDULES'))
alter table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES  drop constraint FKA2F26471918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKA2F2647152514B24]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMER_CHARGE_SCHEDULES'))
alter table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES  drop constraint FKA2F2647152514B24


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKA2F26471DB8EF603]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMER_CHARGE_SCHEDULES'))
alter table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES  drop constraint FKA2F26471DB8EF603


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK1C145B79918EF537]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMER_PRODUCT_ASSN'))
alter table dbo.CRM_CUSTOMER_PRODUCT_ASSN  drop constraint FK1C145B79918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK1C145B79A624DED6]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMER_PRODUCT_ASSN'))
alter table dbo.CRM_CUSTOMER_PRODUCT_ASSN  drop constraint FK1C145B79A624DED6


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK1C145B794D45695C]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMER_PRODUCT_ASSN'))
alter table dbo.CRM_CUSTOMER_PRODUCT_ASSN  drop constraint FK1C145B794D45695C


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBD5506E092203841]') AND parent_object_id = OBJECT_ID('RM_CHARGE_TARGETS'))
alter table RM_CHARGE_TARGETS  drop constraint FKBD5506E092203841


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBD5506E016F0D4F9]') AND parent_object_id = OBJECT_ID('RM_CHARGE_TARGETS'))
alter table RM_CHARGE_TARGETS  drop constraint FKBD5506E016F0D4F9


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKA59E560752514B24]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMER_ACCOUNT_ASSN'))
alter table dbo.CRM_CUSTOMER_ACCOUNT_ASSN  drop constraint FKA59E560752514B24


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKA59E5607918EF537]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMER_ACCOUNT_ASSN'))
alter table dbo.CRM_CUSTOMER_ACCOUNT_ASSN  drop constraint FKA59E5607918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK2E2E8323918EF537]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMERS_CHARGES'))
alter table dbo.CRM_CUSTOMERS_CHARGES  drop constraint FK2E2E8323918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK2E2E832352514B24]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMERS_CHARGES'))
alter table dbo.CRM_CUSTOMERS_CHARGES  drop constraint FK2E2E832352514B24


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK2E2E8323DB8EF603]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMERS_CHARGES'))
alter table dbo.CRM_CUSTOMERS_CHARGES  drop constraint FK2E2E8323DB8EF603


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK2E2E8323DAFB36F4]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMERS_CHARGES'))
alter table dbo.CRM_CUSTOMERS_CHARGES  drop constraint FK2E2E8323DAFB36F4


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK2E2E832392203841]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMERS_CHARGES'))
alter table dbo.CRM_CUSTOMERS_CHARGES  drop constraint FK2E2E832392203841


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK2E2E83236D30F8FE]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMERS_CHARGES'))
alter table dbo.CRM_CUSTOMERS_CHARGES  drop constraint FK2E2E83236D30F8FE


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK2E2E8323B666BB6F]') AND parent_object_id = OBJECT_ID('dbo.CRM_CUSTOMERS_CHARGES'))
alter table dbo.CRM_CUSTOMERS_CHARGES  drop constraint FK2E2E8323B666BB6F


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK4431F07A52514B24]') AND parent_object_id = OBJECT_ID('dbo.CRM_ACCOUNT_INVOICES'))
alter table dbo.CRM_ACCOUNT_INVOICES  drop constraint FK4431F07A52514B24


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK4431F07A918EF537]') AND parent_object_id = OBJECT_ID('dbo.CRM_ACCOUNT_INVOICES'))
alter table dbo.CRM_ACCOUNT_INVOICES  drop constraint FK4431F07A918EF537


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK4431F07A57DDFD24]') AND parent_object_id = OBJECT_ID('dbo.CRM_ACCOUNT_INVOICES'))
alter table dbo.CRM_ACCOUNT_INVOICES  drop constraint FK4431F07A57DDFD24


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK4A68CAC8A624DED6]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCT_RELATIONS'))
alter table dbo.RM_PRODUCT_RELATIONS  drop constraint FK4A68CAC8A624DED6


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK4A68CAC8AC50E958]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCT_RELATIONS'))
alter table dbo.RM_PRODUCT_RELATIONS  drop constraint FK4A68CAC8AC50E958


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK80A8B6B53370A309]') AND parent_object_id = OBJECT_ID('dbo.TAXES'))
alter table dbo.TAXES  drop constraint FK80A8B6B53370A309


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKC8105EBFB666BB6F]') AND parent_object_id = OBJECT_ID('dbo.TAX_RATES'))
alter table dbo.TAX_RATES  drop constraint FKC8105EBFB666BB6F


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKD7E96151B666BB6F]') AND parent_object_id = OBJECT_ID('dbo.TAX_ZIPCODE_RANGES'))
alter table dbo.TAX_ZIPCODE_RANGES  drop constraint FKD7E96151B666BB6F


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK9EB044EAA624DED6]') AND parent_object_id = OBJECT_ID('dbo.PRODUCT_CHARGE_OPTIONS'))
alter table dbo.PRODUCT_CHARGE_OPTIONS  drop constraint FK9EB044EAA624DED6


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK9EB044EA5C0F5C8]') AND parent_object_id = OBJECT_ID('dbo.PRODUCT_CHARGE_OPTIONS'))
alter table dbo.PRODUCT_CHARGE_OPTIONS  drop constraint FK9EB044EA5C0F5C8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FK9EB044EA3370A309]') AND parent_object_id = OBJECT_ID('dbo.PRODUCT_CHARGE_OPTIONS'))
alter table dbo.PRODUCT_CHARGE_OPTIONS  drop constraint FK9EB044EA3370A309


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKE626B9BB950FE7FB]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCTS'))
alter table dbo.RM_PRODUCTS  drop constraint FKE626B9BB950FE7FB


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKE626B9BB5C0F5C8]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCTS'))
alter table dbo.RM_PRODUCTS  drop constraint FKE626B9BB5C0F5C8


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKE626B9BB3370A309]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCTS'))
alter table dbo.RM_PRODUCTS  drop constraint FKE626B9BB3370A309


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKE626B9BBDD7BC768]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCTS'))
alter table dbo.RM_PRODUCTS  drop constraint FKE626B9BBDD7BC768


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKE626B9BB8CAF124E]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCTS'))
alter table dbo.RM_PRODUCTS  drop constraint FKE626B9BB8CAF124E


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKE626B9BB467CA9B]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCTS'))
alter table dbo.RM_PRODUCTS  drop constraint FKE626B9BB467CA9B


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKE626B9BB9B7C8FC]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCTS'))
alter table dbo.RM_PRODUCTS  drop constraint FKE626B9BB9B7C8FC


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'dbo.[FKE626B9BB8E62C593]') AND parent_object_id = OBJECT_ID('dbo.RM_PRODUCTS'))
alter table dbo.RM_PRODUCTS  drop constraint FKE626B9BB8E62C593


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1AB0025C89806304]') AND parent_object_id = OBJECT_ID('RM_PRODUCT_GROUPS'))
alter table RM_PRODUCT_GROUPS  drop constraint FK1AB0025C89806304


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK1AB0025CA624DED6]') AND parent_object_id = OBJECT_ID('RM_PRODUCT_GROUPS'))
alter table RM_PRODUCT_GROUPS  drop constraint FK1AB0025CA624DED6


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3E60F2C8CAF124E]') AND parent_object_id = OBJECT_ID('CRM_BUSINESSRULES_PACKAGES'))
alter table CRM_BUSINESSRULES_PACKAGES  drop constraint FK3E60F2C8CAF124E


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK3E60F2C235024C6]') AND parent_object_id = OBJECT_ID('CRM_BUSINESSRULES_PACKAGES'))
alter table CRM_BUSINESSRULES_PACKAGES  drop constraint FK3E60F2C235024C6


    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_ACTIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_ACTIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_ALERTRULE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_ALERTRULE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_ALERTRULE_MESSAGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_ALERTRULE_MESSAGE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_ALERTRULE_MESSAGE_TEMPLATE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_ALERTRULE_MESSAGE_TEMPLATE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_AUTOCREDITTRANSFERSETTING') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_AUTOCREDITTRANSFERSETTING

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_BANK') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_BANK

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_BLOCK_MSISDN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_BLOCK_MSISDN

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_BRS_CHANNEL') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_BRS_CHANNEL

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_BRS_CURRENCY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_BRS_CURRENCY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_BRS_FINANCIALENTITY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_BRS_FINANCIALENTITY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_BRS_TAXCODE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_BRS_TAXCODE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_BRS_TAXCODE_POSTCODE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_BRS_TAXCODE_POSTCODE

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_BUNDLE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_BUNDLE

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_BUNDLE_PACKAGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_BUNDLE_PACKAGE

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_CALLDIRECTION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_CALLDIRECTION

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_CALLTYPE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_CALLTYPE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_PROMOTION_PROPERTY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_PROMOTION_PROPERTY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_BONUS_RELATIONSHIP') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_BONUS_RELATIONSHIP

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_BUNDLE_ASSIGNMENT_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_BUNDLE_ASSIGNMENT_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_DATES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_DATES

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_EXTRAUSAGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_EXTRAUSAGE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_FF_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_FF_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_FF') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_FF

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_FF_INITIALCHARGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_FF_INITIALCHARGE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_MSISDNGROUP_MEMBERS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_MSISDNGROUP_MEMBERS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_MSISDNGROUP_MEMBERS_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_MSISDNGROUP_MEMBERS_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_PROMOTIONGROUPS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_PROMOTIONGROUPS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_PROMOTION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_PROMOTION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_PROMOTION_OPERATION_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_PROMOTION_OPERATION_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_RESOURCEMB') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_RESOURCEMB

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_RESOURCEMB_PROPERTY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_RESOURCEMB_PROPERTY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_SPECIFICNUMBER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_SPECIFICNUMBER

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_TOPUP_BONUS_APPLY_COUNT') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_TOPUP_BONUS_APPLY_COUNT

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_TOPUP_BONUS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_TOPUP_BONUS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_TOPUP_PROMOTION_APPLY_COUNT') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_TOPUP_PROMOTION_APPLY_COUNT

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_TOPUP_PROMOTION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_TOPUP_PROMOTION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_UNPAIDFEE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_UNPAIDFEE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_SETTINGS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_SETTINGS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEFAULT_PROVISION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEFAULT_PROVISION

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_MESSAGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_MESSAGE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_PROMOTED_TOPUP_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_PROMOTED_TOPUP_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_TOPUP_BONUS_DETAIL') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_TOPUP_BONUS_DETAIL

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_TOPUP_BONUS_FAILED_PROMOTION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_TOPUP_BONUS_FAILED_PROMOTION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_TOPUP_BONUS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_TOPUP_BONUS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_TOPUP_BONUS_PROMOTION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_TOPUP_BONUS_PROMOTION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_TOPUPPROMOTION_CONFIG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_TOPUPPROMOTION_CONFIG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_CREDITCARDS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_CREDITCARDS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_DATA_ROAMING_LIMIT') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_DATA_ROAMING_LIMIT

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_DATA_ROAMING_LIMIT_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_DATA_ROAMING_LIMIT_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_DATA_ROAMING_LIMIT_NOTIFICATION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_DATA_ROAMING_LIMIT_NOTIFICATION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_ADDRESSES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_ADDRESSES

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_DATECATEGORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_DATECATEGORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_TOPUP_CONFIG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_TOPUP_CONFIG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_BANK') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_BANK

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_ICCID') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_ICCID

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_IMSI') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_IMSI

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_LOYALTY_POINT') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_LOYALTY_POINT

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_PROPERTIES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_PROPERTIES

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_PRS_DEALER_RATEPLAN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_PRS_DEALER_RATEPLAN

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_VOUCHER_MVNO') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_VOUCHER_MVNO

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS_DICTIONARY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS_DICTIONARY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_DID') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_DID

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DRE_OPERATION_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DRE_OPERATION_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_EMAIL_ATTACHMENT_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_EMAIL_ATTACHMENT_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_EMAIL_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_EMAIL_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_EVENTS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_EVENTS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_EXECUTE_TRACE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_EXECUTE_TRACE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_EXTERNAL_RESOURCE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_EXTERNAL_RESOURCE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_TOPUP_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_TOPUP_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_HOLIDAY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_HOLIDAY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_RESMB_HUNTING_GROUP_DEST') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_RESMB_HUNTING_GROUP_DEST

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_RESMB_HUNTING_GROUP') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_RESMB_HUNTING_GROUP

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_HUNTING_GROUP_SETTING') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_HUNTING_GROUP_SETTING

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_ID_MAPPING') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_ID_MAPPING

    if exists (select * from dbo.sysobjects where id = object_id(N'MSISDN_IMEI_ASSN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MSISDN_IMEI_ASSN

    if exists (select * from dbo.sysobjects where id = object_id(N'MSISDN_IMEI_ASSN_HIST') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MSISDN_IMEI_ASSN_HIST

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MOBILE_SIMCARDS_IMPORT_DETAILS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MOBILE_SIMCARDS_IMPORT_DETAILS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MOBILE_SIMCARDS_IMPORT_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MOBILE_SIMCARDS_IMPORT_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MOBILE_SIMCARDS_IMPORT_MASTER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MOBILE_SIMCARDS_IMPORT_MASTER

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PRS_INCUMBENT') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PRS_INCUMBENT

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_INTERFACE_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_INTERFACE_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_INTERNATIONAL_ZONE_DETAILS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_INTERNATIONAL_ZONE_DETAILS

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_INTERNATIONAL_ZONE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_INTERNATIONAL_ZONE

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_ROAMING_GSNIP_RANGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_ROAMING_GSNIP_RANGE

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS_LANGUAGETYPE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS_LANGUAGETYPE

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_LOG_LEVEL') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_LOG_LEVEL

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_HOLIDAY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_HOLIDAY

    if exists (select * from dbo.sysobjects where id = object_id(N'MVNO_FC_LIFECYCLE_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MVNO_FC_LIFECYCLE_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'MVNO_FC_LIFECYCLE_LOG_EXTEND') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table MVNO_FC_LIFECYCLE_LOG_EXTEND

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_STATUS_TRANSITION_NOTIFICATION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_STATUS_TRANSITION_NOTIFICATION

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_USER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_USER

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_MAPPING') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_MAPPING

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS_MESSAGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS_MESSAGE

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS_MESSAGE_USER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS_MESSAGE_USER

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_CONSOLIDATION_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_CONSOLIDATION_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_MNP_DELETION_REQUEST') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_MNP_DELETION_REQUEST

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_INCOMING_EFFECT') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_INCOMING_EFFECT

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_NPDB_ESVF') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_NPDB_ESVF

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_ORIGINAL_NRN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_ORIGINAL_NRN

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_OPERATOR_NRN_RANGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_OPERATOR_NRN_RANGE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_PORTABILITY_CUSTOMER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_PORTABILITY_CUSTOMER

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_PORTABILITY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_PORTABILITY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_PORTABILITY_MULTI') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_PORTABILITY_MULTI

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_PORTABILITY_ATTACH') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_PORTABILITY_ATTACH

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_REASON') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_REASON

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MNP_SessionInfo') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MNP_SessionInfo

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_MODULE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_MODULE

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_MODULE_ELEMENTS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_MODULE_ELEMENTS

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_MODULE_MVNO') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_MODULE_MVNO

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS_MULTILINGUAL') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS_MULTILINGUAL

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_APN_IP_BATCH_INFO') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_APN_IP_BATCH_INFO

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_APN_IP_POOL') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_APN_IP_POOL

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_AUTO_TOPUP_NOTIFICATION_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_AUTO_TOPUP_NOTIFICATION_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_AUTO_TOPUP_NOTIFICATION_PROVISION_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_AUTO_TOPUP_NOTIFICATION_PROVISION_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_AUTO_TOPUP_NOTIFICATION_QUEUE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_AUTO_TOPUP_NOTIFICATION_QUEUE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_CONFIGACTION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_CONFIGACTION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_CUSTOMERS_PROPERTY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_CUSTOMERS_PROPERTY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_DATA_ROAMING_LIMIT') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_DATA_ROAMING_LIMIT

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_DATA_ROAMING_LIMIT_NOTIFICATION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_DATA_ROAMING_LIMIT_NOTIFICATION

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS_MVNO_LANGUAGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS_MVNO_LANGUAGE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_NOTIFICATION_SETTINGS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_NOTIFICATION_SETTINGS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_PROMOTION_SMS_CONFIG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_PROMOTION_SMS_CONFIG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_PROPERTIES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_PROPERTIES

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_SMS_ACCOUNT') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_SMS_ACCOUNT

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_BUNDLE_PACKAGE_TOPUP_BASED') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_BUNDLE_PACKAGE_TOPUP_BASED

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS_NPM_BATCH_INFO') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS_NPM_BATCH_INFO

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS_NPM_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS_NPM_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_GEO_LOCATION_NUMBER_RANGES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_GEO_LOCATION_NUMBER_RANGES

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_OPERATION_DETAILS_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_OPERATION_DETAILS_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_LOG_OPERATION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_LOG_OPERATION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_OPERATION_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_OPERATION_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_ORGANIZATION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_ORGANIZATION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_PACKAGE_MAPPING') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_PACKAGE_MAPPING

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PACKAGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PACKAGE

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PACKAGE_RELATIONSHIPS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PACKAGE_RELATIONSHIPS

    if exists (select * from dbo.sysobjects where id = object_id(N'Sys25_Performance_Trace_Config') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Sys25_Performance_Trace_Config

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS30_PERMISSION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS30_PERMISSION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MOBILE_SIMCARD_PREACTIVATE_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MOBILE_SIMCARD_PREACTIVATE_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'VF_FILE_PRICEPLAN_PACKAGEID') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table VF_FILE_PRICEPLAN_PACKAGEID

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_PRODUCTS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_PRODUCTS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_PROPERTY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_PROPERTY

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_RATEPLAN_DETAILS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_RATEPLAN_DETAILS

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_RATEPLAN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_RATEPLAN

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_TRANSLATION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_TRANSLATION

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_REGEX') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_REGEX

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_RELATE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_RELATE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_REPORTRESOURCES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_REPORTRESOURCES

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_BLACKWHITELIST') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_BLACKWHITELIST

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_FF_FEE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_FF_FEE

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_FF_OPERATORLIST') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_FF_OPERATORLIST

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_FIX_NUMBERRANGE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_FIX_NUMBERRANGE

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PROMOTIONGROUPS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PROMOTIONGROUPS

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PROMOTIONGROUP_MEMBERS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PROMOTIONGROUP_MEMBERS

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PROMOTIONPLAN_DETAIL') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PROMOTIONPLAN_DETAIL

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PROMOTIONPLAN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PROMOTIONPLAN

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PROMOTIONPLAN_RULE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PROMOTIONPLAN_RULE

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PROMOTIONPLAN_TOPUP_BASED_PARAMETERS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PROMOTIONPLAN_TOPUP_BASED_PARAMETERS

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_SPECIFICNUMBER_GROUP') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_SPECIFICNUMBER_GROUP

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_ROAMING_BLACKLIST') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_ROAMING_BLACKLIST

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_ROAMING_OPERATORS_E164') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_ROAMING_OPERATORS_E164

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_ROAMING_SETTING') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_ROAMING_SETTING

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_ROAMING_SMS_CONFIG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_ROAMING_SMS_CONFIG

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_Roaming_SMS_Histrory') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_Roaming_SMS_Histrory

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_ROAMING_ZONE_DETAILS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_ROAMING_ZONE_DETAILS

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_ROAMING_ZONE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_ROAMING_ZONE

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_ROLE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_ROLE

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_ROLE_PERMISSION') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_ROLE_PERMISSION

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_SERVICES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_SERVICES

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_SETTINGS_DETAILS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_SETTINGS_DETAILS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_SETTINGS_EXTEND_DETAILS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_SETTINGS_EXTEND_DETAILS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_SETTINGS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_SETTINGS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_IMSI_MAP') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_IMSI_MAP

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MOBILE_SIMCARDS_ORDER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MOBILE_SIMCARDS_ORDER

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_SMS_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_SMS_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_SMS_QUEUE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_SMS_QUEUE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_TEXT_TEMPLATE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_TEXT_TEMPLATE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_SMS_TEMPLETS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_SMS_TEMPLETS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_SPECIALTELNUMBER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_SPECIALTELNUMBER

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_RESOURCE_STATUSCHANGED_LOG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_RESOURCE_STATUSCHANGED_LOG

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_STYLE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_STYLE

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_CONFIG_DATA') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_CONFIG_DATA

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS_ID') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS_ID

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_THEME') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_THEME

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_TIMECATEGORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_TIMECATEGORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_TRANSFER_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_TRANSFER_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_LIFECYCLE_TRANSITIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_LIFECYCLE_TRANSITIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_TROUBLE_TICKET') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_TROUBLE_TICKET

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_TT_QUESTION_LIB') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_TT_QUESTION_LIB

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_TT_DEPT') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_TT_DEPT

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_TT_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_TT_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_UNITCATEGORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_UNITCATEGORY

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_USER_DEALER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_USER_DEALER

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_USER_ORG') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_USER_ORG

    if exists (select * from dbo.sysobjects where id = object_id(N'SYS25_USER_ROLE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table SYS25_USER_ROLE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_VOUCHER_BATCH_INFO') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_VOUCHER_BATCH_INFO

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_VOUCHER_BAK') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_VOUCHER_BAK

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_VOUCHER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_VOUCHER

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_VPN_BUSINESS_GROUP') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_VPN_BUSINESS_GROUP

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_VPN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_VPN

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_VPN_SHORTCODE') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_VPN_SHORTCODE

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MVNO_VPN_SUBSCRIBER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MVNO_VPN_SUBSCRIBER

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEALERS_OBOPRS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEALERS_OBOPRS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MOBILE_MULTIPLEIMSI') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MOBILE_MULTIPLEIMSI

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MOBILE_NETWORK') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MOBILE_NETWORK

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEFAULT_PROVISION_NETWORK') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEFAULT_PROVISION_NETWORK

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_DEFAULT_PROVISION_ROAMING_SETTING') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_DEFAULT_PROVISION_ROAMING_SETTING

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_REMARKS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_REMARKS

    if exists (select * from dbo.sysobjects where id = object_id(N'HLR_RequestErrors') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table HLR_RequestErrors

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_BUSINESSRULES_PROMOTIONGROUPS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_BUSINESSRULES_PROMOTIONGROUPS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.ADDRESSES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.ADDRESSES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.TEXT_LOOKUP_TRANSLATIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.TEXT_LOOKUP_TRANSLATIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.TEXT_LOOKUPS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.TEXT_LOOKUPS

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_OPERATORS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_OPERATORS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_OPERATION_EXECUTIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_OPERATION_EXECUTIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_OPERATIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_OPERATIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_OPERATION_CONFIGURATIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_OPERATION_CONFIGURATIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_ORDERS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_ORDERS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_ORDER_STATUS_HISTORY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_ORDER_STATUS_HISTORY

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_CUSTOMERS_PROMOTION_LIMITS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_CUSTOMERS_PROMOTION_LIMITS

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.RM_PROMOTIONGROUP_THRESHOLDS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.RM_PROMOTIONGROUP_THRESHOLDS

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.SYS_NPM_DEALER') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.SYS_NPM_DEALER

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.SYS_NPM') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.SYS_NPM

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.SYS_NPM_PROPERTY') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.SYS_NPM_PROPERTY

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_MOBILE_SIMCARDS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_MOBILE_SIMCARDS

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.CRM_ACCOUNTS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.CRM_ACCOUNTS

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.CRM_ACCOUNT_BALANCES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.CRM_ACCOUNT_BALANCES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.RM_BILL_CYCLES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.RM_BILL_CYCLES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.RM_BILL_RUN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.RM_BILL_RUN

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.RM_CHARGES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.RM_CHARGES

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_CHARGE_OPTIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_CHARGE_OPTIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.RM_CHARGE_PRICES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.RM_CHARGE_PRICES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.CRM_CUSTOMER_CHARGE_SCHEDULES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.CRM_CUSTOMER_PRODUCT_ASSN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.CRM_CUSTOMER_PRODUCT_ASSN

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_CHARGE_TARGETS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_CHARGE_TARGETS

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.CRM_CUSTOMER_ACCOUNT_ASSN') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.CRM_CUSTOMER_ACCOUNT_ASSN

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.CRM_CUSTOMERS_CHARGES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.CRM_CUSTOMERS_CHARGES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.CRM_ACCOUNT_INVOICES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.CRM_ACCOUNT_INVOICES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.RM_PRODUCT_RELATIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.RM_PRODUCT_RELATIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.PRODUCTTYPES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.PRODUCTTYPES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.TAXES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.TAXES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.TAX_RATES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.TAX_RATES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.TAX_ZIPCODE_RANGES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.TAX_ZIPCODE_RANGES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.PRODUCT_CHARGE_OPTIONS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.PRODUCT_CHARGE_OPTIONS

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.RM_PRODUCTS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.RM_PRODUCTS

    if exists (select * from dbo.sysobjects where id = object_id(N'RM_PRODUCT_GROUPS') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table RM_PRODUCT_GROUPS

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_BUSINESSRULES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_BUSINESSRULES

    if exists (select * from dbo.sysobjects where id = object_id(N'CRM_BUSINESSRULES_PACKAGES') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table CRM_BUSINESSRULES_PACKAGES

    if exists (select * from dbo.sysobjects where id = object_id(N'dbo.USAGE_DETAIL') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table dbo.USAGE_DETAIL

    create table CRM_LIFECYCLE_ACTIONS (
        ACTIONId INT not null,
       DEALERId INT not null,
       ASSEMBLYNAME NVARCHAR(60) null,
       CLASSNAME NVARCHAR(240) null,
       METHODNAME NVARCHAR(100) null,
       DESCRIPTION NVARCHAR(1000) null,
       PARAMETERS NVARCHAR(200) null,
       primary key (ACTIONId, DEALERId)
    )

    create table CRM_ALERTRULE (
        ALERTRULEID INT not null,
       MNVOID INT null,
       MNVONAME NVARCHAR(20) null,
       ALERTRULENAME NVARCHAR(50) null,
       MATTERNAME NVARCHAR(50) null,
       ISACTIVE INT null,
       PARAMETERSNAME NVARCHAR(50) null,
       PARAMETERSTYPE NVARCHAR(20) null,
       PARAMETERSVALUES NVARCHAR(50) null,
       PARAMETERUNIT NVARCHAR(20) null,
       NAME1 NVARCHAR(20) null,
       NAME2 NVARCHAR(20) null,
       NAME3 NVARCHAR(20) null,
       NAME4 NVARCHAR(20) null,
       NAME5 NVARCHAR(20) null,
       VALUES1 NVARCHAR(30) null,
       VALUES2 NVARCHAR(30) null,
       VALUES3 NVARCHAR(30) null,
       VALUES4 NVARCHAR(30) null,
       VALUES5 NVARCHAR(30) null,
       CREATEUSER NVARCHAR(50) null,
       CREATEDATE DATETIME null,
       UPDATEUSER NVARCHAR(50) null,
       UPDATEDATE DATETIME null,
       USERID INT null,
       primary key (ALERTRULEID)
    )

    create table CRM_ALERTRULE_MESSAGE (
        MESSAGEID INT not null,
       MNVOID INT null,
       MNVONAME NVARCHAR(20) null,
       ALERTRULENAME NVARCHAR(50) null,
       MATTERNAME NVARCHAR(50) null,
       MESSAGETYPE INT null,
       ISSEND INT null,
       UNIT NVARCHAR(30) null,
       UNITHOURS NVARCHAR(20) null,
       UNITDATE NVARCHAR(20) null,
       SENDS NVARCHAR(100) null,
       RECEIVERS NVARCHAR(200) null,
       DESCRIPTION NVARCHAR(200) null,
       CREATEUSER NVARCHAR(50) null,
       CREATEDATE DATETIME null,
       UPDATEUSER NVARCHAR(50) null,
       UPDATEDATE DATETIME null,
       USERID INT null,
       primary key (MESSAGEID)
    )

    create table CRM_ALERTRULE_MESSAGE_TEMPLATE (
        TEMPLATEID INT not null,
       MNVOID INT null,
       ALERTRULENAME NVARCHAR(50) null,
       MESSAGEID INT null,
       TEMPLATETYPE INT null,
       TEMPLATELANGUAGE INT null,
       TEMPLATENAME NVARCHAR(60) null,
       TEMPLATETHEME NVARCHAR(200) null,
       TEMPLATETEXT NVARCHAR(400) null,
       DESCRIPTION NVARCHAR(400) null,
       primary key (TEMPLATEID)
    )

    create table CRM_CUSTOMERS_AUTOCREDITTRANSFERSETTING (
        ID INT not null,
       MVNOID INT null,
       DEALERID INT null,
       CUSTOMERID INT null,
       MSISDNFROM NVARCHAR(25) null,
       MSISDNTO NVARCHAR(25) null,
       AMOUNT DECIMAL(19,5) null,
       AMOUNTWITHOUTTAX DECIMAL(19,5) null,
       TAXCODE INT null,
       PRIORITY INT null,
       STATUS INT null,
       USERID INT null,
       CREATEDATE DATETIME null,
       UPDATEDATE DATETIME null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_BANK (
        BANKID INT not null,
       BANKCODE NVARCHAR(25) null,
       BANKNAME NVARCHAR(200) null,
       BANKNUMBER NVARCHAR(50) null,
       OWNER NVARCHAR(200) null,
       CITY NVARCHAR(200) null,
       COUNTRYID INT null,
       IBAN NVARCHAR(100) null,
       SWIFT NVARCHAR(100) null,
       CVC NVARCHAR(20) null,
       ABI NVARCHAR(10) null,
       CAB NVARCHAR(10) null,
       VALIDDATE NVARCHAR(7) null,
       CREATEDATE DATETIME null,
       USERID INT null,
       ACCOUNTCODE NVARCHAR(50) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CUSTOMERID INT null,
       primary key (BANKID)
    )

    create table CRM_BLOCK_MSISDN (
        ID BIGINT not null,
       DEALERID INT null,
       CUSTOMERID INT null,
       MSISDN NVARCHAR(25) null,
       TOPUPTYPE NVARCHAR(50) null,
       TOTALTOPUPTIMES INT null,
       TOTALTOPUPAMOUNT DECIMAL(19,5) null,
       TIMEUNIT NVARCHAR(25) null,
       CREATEUSERID INT null,
       CREATEDATE DATETIME null,
       STATUSID INT null,
       RULETYPE INT null,
       AMOUNT DECIMAL(19,5) null,
       ISSEND INT null,
       primary key (ID)
    )

    create table CRM_MVNO_BRS_CHANNEL (
        CHANNELCODE INT IDENTITY NOT NULL,
       CHANNELNAME NVARCHAR(20) null,
       DESCRIPTION NVARCHAR(1000) null,
       primary key (CHANNELCODE)
    )

    create table CRM_MVNO_BRS_CURRENCY (
        CURRENCYID INT IDENTITY NOT NULL,
       CODENUMBER INT null,
       CODESTRING NVARCHAR(10) null,
       DESCRIPTION NVARCHAR(1000) null,
       primary key (CURRENCYID)
    )

    create table CRM_MVNO_BRS_FINANCIALENTITY (
        ENTITYCODE INT IDENTITY NOT NULL,
       ENTITYNAME NVARCHAR(20) null,
       DESCRIPTION NVARCHAR(1000) null,
       primary key (ENTITYCODE)
    )

    create table CRM_MVNO_BRS_TAXCODE (
        TAXCODE INT IDENTITY NOT NULL,
       TAXVALUE FLOAT(53) null,
       DESCRIPTION NVARCHAR(1000) null,
       primary key (TAXCODE)
    )

    create table CRM_MVNO_BRS_TAXCODE_POSTCODE (
        SeqID INT not null,
       TAXCODE INT null,
       POSTCODE NVARCHAR(10) null,
       primary key (SeqID)
    )

    create table RM_BUNDLE (
        BUNDLEID INT not null,
       DEALERID INT null,
       SERVICETYPEID INT null,
       SUBSERVICETYPEID INT null,
       BUNDLENAME NVARCHAR(400) null,
       RATEPLANID INT null,
       CREDITLIMIT DECIMAL(19,5) null,
       SUBSCRIPTIONFEE DECIMAL(19,5) null,
       SUBSCRIPTIONCYCLEID INT null,
       CURRENCYID INT null,
       PAYMENTTYPEID INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       MONTHFEE DECIMAL(19,5) null,
       primary key (BUNDLEID)
    )

    create table RM_BUNDLE_PACKAGE (
        RELATIONID INT not null,
       BUNDLEID INT null,
       PackageID INT null,
       primary key (RELATIONID)
    )

    create table RM_CALLDIRECTION (
        CALLDIRECTIONID INT not null,
       CALLDIRECTIONNAME NVARCHAR(50) null,
       primary key (CALLDIRECTIONID)
    )

    create table RM_CALLTYPE (
        CALLTYPEID INT not null,
       CALLTYPENAME NVARCHAR(50) null,
       primary key (CALLTYPEID)
    )

    create table CRM_CUSTOMERS_PROMOTION_PROPERTY (
        PROMOTIONID BIGINT not null,
       ISSENT INT null,
       primary key (PROMOTIONID)
    )

    create table CRM_CUSTOMERS_BONUS_RELATIONSHIP (
        ID INT not null,
       MVNOID INT null,
       Description NVARCHAR(400) null,
       RelationShipTypeID INT null,
       BeRelationShipedID INT null,
       BonusPromotionID INT null,
       BonusAmount DECIMAL(19,5) null,
       BounsValidityType INT null,
       BonusValidityValue INT null,
       StartDate DATETIME null,
       EndDate DATETIME null,
       CreateDate DATETIME null,
       CreateUserID INT null,
       Status INT null,
       UpdateDate DATETIME null,
       UpdateUserID INT null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_BUNDLE_ASSIGNMENT_HISTORY (
        CUSTOMERID INT not null,
       SERVICEID INT not null,
       BUNDLEID INT not null,
       STARTDATE DATETIME not null,
       CREDITLIMITBASEBUNDLEID INT null,
       ENDDATE DATETIME null,
       TOPUP_HISTORY_ID BIGINT null,
       primary key (CUSTOMERID, SERVICEID, BUNDLEID, STARTDATE)
    )

    create table CRM_CUSTOMERS_DATES (
        CUSTOMERID INT not null,
       DATETYPEID INT not null,
       DATEVALUE DATETIME null,
       primary key (CUSTOMERID, DATETYPEID)
    )

    create table CRM_CUSTOMERS_EXTRAUSAGE (
        ID BIGINT not null,
       DEALERID INT null,
       CUSTOMERID INT null,
       TYPEID INT null,
       AMOUNT DECIMAL(19,5) null,
       CURRENCYID INT null,
       TAXCODE INT null,
       AMOUNTWITHTAX DECIMAL(19,5) null,
       ISBILLED BIT null,
       BILLEDTIME DATETIME null,
       CREATEDATE DATETIME null,
       USERID INT null,
       PROMOTIONPLANID INT null,
       PAYMENTTYPE INT null,
       STATUS INT null,
       DESCRIPTION NVARCHAR(1000) null,
       BAK1 NVARCHAR(200) null,
       BAK2 NVARCHAR(200) null,
       BAK3 NVARCHAR(200) null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_FF_HISTORY (
        CUSTOMERID INT not null,
       MSISDN NVARCHAR(25) null,
       ACTIONTYPE INT null,
       ACTIONDATE DATETIME null,
       CURRENTNUMBER1 NVARCHAR(25) null,
       CURRENTNUMBER2 NVARCHAR(25) null,
       USERID INT null,
       primary key (CUSTOMERID)
    )

    create table CRM_CUSTOMERS_FF (
        FFID INT not null,
       CUSTOMERID INT null,
       RESOURCE NVARCHAR(25) null,
       CustomerId2 INT null,
       MSISDN2 NVARCHAR(25) null,
       PromotionPlanID INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CREATEDATE DATETIME null,
       USERID INT null,
       primary key (FFID)
    )

    create table CRM_CUSTOMERS_FF_INITIALCHARGE (
        CUSTOMERID INT not null,
       CATEGORYID INT not null,
       CHARGEDATE DATETIME null,
       primary key (CUSTOMERID, CATEGORYID)
    )

    create table CRM_CUSTOMERS_MSISDNGROUP_MEMBERS (
        CUSTOMERID INT not null,
       MSISDNGROUPTYPEID INT not null,
       MSISDN NVARCHAR(255) not null,
       STARTDATE DATETIME null,
       primary key (CUSTOMERID, MSISDNGROUPTYPEID, MSISDN)
    )

    create table CRM_CUSTOMERS_MSISDNGROUP_MEMBERS_HISTORY (
        CUSTOMERID INT not null,
       MSISDNGROUPTYPEID INT not null,
       MSISDN NVARCHAR(255) not null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       primary key (CUSTOMERID, MSISDNGROUPTYPEID, MSISDN)
    )

    create table CRM_CUSTOMERS_PROMOTIONGROUPS (
        ID BIGINT not null,
       CUSTOMERID INT null,
       STATUSID INT null,
       STARTDATE DATETIME null,
       PROMOTIONGROUPID INT null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_PROMOTION (
        PROMOTIONID BIGINT not null,
       STARTDATE DATETIME null,
       CURRENTLIMIT DECIMAL(19,5) null,
       ACTIVE BIT null,
       ENDDATE DATETIME null,
       WHITELIST NVARCHAR(500) null,
       FIRSTUSED DATETIME null,
       RENEWALCOUNT INT null,
       RENEWAUTOMATICALLY INT null,
       DEACTIVEREASON INT null,
       BATCHNO NVARCHAR(25) null,
       BATCHID BIGINT null,
       RENEWDATE DATETIME null,
       INITIALLIMIT DECIMAL(19,5) null,
       PRIORITY INT null,
       ACTIVEWITHOUTCREDIT INT null,
       NEXT_RENEWAL_DATE DATETIME null,
       NEXT_PERIOD_NUMBER INT null,
       CURRENT_CYCLE_NUMBER INT null,
       PRERENEWALACTIONS_DATE DATETIME null,
       PREACTIONSEXECUTED BIT null,
       ACTIONSEXECUTED BIT null,
       CUSTOMERID INT null,
       PROMOTIONPLANDETAILID INT null,
       primary key (PROMOTIONID)
    )

    create table CRM_CUSTOMERS_PROMOTION_OPERATION_LOG (
        LOGID BIGINT not null,
       PRELOGID BIGINT null,
       OPERATIONDATE DATETIME null,
       OPCODE INT null,
       DEALERID INT null,
       MSISDN NVARCHAR(50) null,
       CUSTOMERID INT null,
       OPERATIONCOST DECIMAL(19,5) null,
       PROMOTIONPLANID INT null,
       STATUS NVARCHAR(10) null,
       APRIORITY INT null,
       ASTARTDATE DATETIME null,
       AENDDATE DATETIME null,
       AAMOUNT DECIMAL(19,5) null,
       ANEXTRENEWDATE DATETIME null,
       BPRIORITY INT null,
       BSTARTDATE DATETIME null,
       BENDDATE DATETIME null,
       BAMOUNT DECIMAL(19,5) null,
       BNEXTRENEWDATE DATETIME null,
       DESCRIPTION NVARCHAR(300) null,
       PROMOTIONPLANDETAILID INT null,
       PromotionGroupID INT null,
       OperationCode NVARCHAR(50) null,
       primary key (LOGID)
    )

    create table CRM_CUSTOMERS_RESOURCEMB (
        RESOURCEID INT not null,
       RESOURCE NVARCHAR(25) null,
       ICC NVARCHAR(25) null,
       IMSI NVARCHAR(15) null,
       REMARKS NVARCHAR(100) null,
       MSISDNALERTIND NVARCHAR(15) null,
       ODBMASK NVARCHAR(500) null,
       USSDALLOWED BIT null,
       CB_SUBSOPTION INT null,
       CB_PASSWORD NVARCHAR(50) null,
       CB_WRONGATTEMPTS INT null,
       CALCULATION INT null,
       STATUSID INT null,
       FIRSTUSED DATETIME null,
       LASTUSED DATETIME null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CREATEDATE DATETIME null,
       USERID INT null,
       PUK NVARCHAR(25) null,
       TELESERVICELIST NVARCHAR(200) null,
       BEARERSERVICELIST NVARCHAR(200) null,
       CHANGESTATUSDATE DATETIME null,
       LASTCONSUMEDATE DATETIME null,
       ACTIVEDEADLINEDATE DATETIME null,
       PININVALIDTIMES INT null,
       PININVALIDTIMESTOTAL INT null,
       OCPLMNTEMPLATE INT null,
       PROVISIONID INT null,
       NAM INT null,
       FTNRule NVARCHAR(15) null,
       CUSTOMERID INT null,
       OperatorCode INT null,
       FROZENDATE DATETIME null,
       BBROAMINGSTATUS INT null,
       ACTIVATIONCHANNEL INT null,
       primary key (RESOURCEID)
    )

    create table CRM_CUSTOMERS_RESOURCEMB_PROPERTY (
        PROPERTYID INT not null,
       SMSSTATUS BIT null,
       MAXDAILYDATAONROAMING DECIMAL(19,5) null,
       PREPAIDBALANCELOWERSENDSMSSTATUS INT null,
       PREPAIDBALANCEEXHAUSIONSENDSMSSTATUS INT null,
       POSTPAIDCREDITLOWERSENDSMSSTATUS INT null,
       POSTPAIDCREDITEXHAUSIONSENDSMSSTATUS INT null,
       CHANGEPREPAIDBALANCELOWERSENDSMSSTATUSTIME DATETIME null,
       CHANGEPREPAIDBALANCEEXHAUSIONSENDSMSSTATUSTIME DATETIME null,
       CHANGEPOSTPAIDCREDITLOWERSENDSMSSTATUSTIME DATETIME null,
       CHANGEPOSTPAIDCREDITEXHAUSIONSENDSMSSTATUSTIME DATETIME null,
       RESOURCEID INT null,
       primary key (PROPERTYID)
    )

    create table CRM_CUSTOMERS_SPECIFICNUMBER (
        CustomersSpecificNumberId INT not null,
       CustomerId INT null,
       GROUPID INT null,
       SPECIFICNUMBER NVARCHAR(25) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       primary key (CustomersSpecificNumberId)
    )

    create table CRM_CUSTOMERS_TOPUP_BONUS_APPLY_COUNT (
        ID INT not null,
       RESOURCE NVARCHAR(50) null,
       BONUSID INT null,
       YEAR NVARCHAR(50) null,
       MONTH NVARCHAR(50) null,
       CUSTOMERID INT null,
       COUNT INT null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_TOPUP_BONUS (
        TOPUPBONUSID INT not null,
       TOPUPID BIGINT null,
       DEALERID INT null,
       BONUSID INT null,
       CUSTOMERID INT null,
       RESOURCE NVARCHAR(50) null,
       BONUSAMOUNT DECIMAL(19,5) null,
       BONUSAMOUNTWITHTAX DECIMAL(19,5) null,
       ACTIVE BIT null,
       APPLYDATE DATETIME null,
       BONUSTOPUPID BIGINT null,
       primary key (TOPUPBONUSID)
    )

    create table CRM_CUSTOMERS_TOPUP_PROMOTION_APPLY_COUNT (
        ID INT not null,
       RESOURCE NVARCHAR(50) null,
       BONUSID INT null,
       BONUSPROMOTIONID INT null,
       YEAR NVARCHAR(50) null,
       MONTH NVARCHAR(50) null,
       CUSTOMERID INT null,
       COUNT INT null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_TOPUP_PROMOTION (
        TOPUPPROMOTIONID INT not null,
       PROCESSID INT null,
       TOPUPID BIGINT null,
       DEALERID INT null,
       BONUSID INT null,
       BONUSPROMOTIONID INT null,
       CUSTOMERID INT null,
       RESOURCE NVARCHAR(50) null,
       PROMOTIONPLANID INT null,
       ACTIVE BIT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       APPLYDATE DATETIME null,
       primary key (TOPUPPROMOTIONID)
    )

    create table CRM_CUSTOMERS_UNPAIDFEE (
        CUSTOMERID INT not null,
       PROMOTIONPLANID INT not null,
       FEEDATE DATETIME not null,
       FEETYPE INT null,
       AMOUNT DECIMAL(19,5) null,
       STATUSID INT null,
       TRYCHARGEDATE DATETIME null,
       REMARK NVARCHAR(200) null,
       BATCHID BIGINT null,
       primary key (CUSTOMERID, PROMOTIONPLANID, FEEDATE)
    )

    create table CRM_DEALERS_SETTINGS (
        SETTINGID INT not null,
       DEALERID INT null,
       LANGUAGEID INT null,
       ITEMSEQ INT null,
       ITEMID INT null,
       ITEMINDEX INT null,
       ITEMVALUE NVARCHAR(1600) null,
       ITEMLINK NVARCHAR(400) null,
       ITEMDESCRIPTION NVARCHAR(400) null,
       primary key (SETTINGID)
    )

    create table CRM_DEFAULT_PROVISION (
        PROVISIONID INT not null,
       PROVISIONAME NVARCHAR(100) null,
       DEALERID INT null,
       HLRSERVERIP NVARCHAR(50) null,
       HLRSERVERPORT INT null,
       HLRSERVERUSER NVARCHAR(50) null,
       HLRSERVERPWD NVARCHAR(150) null,
       VMS NVARCHAR(20) null,
       HELPDESK NVARCHAR(20) null,
       TELEPHONENUMBERS INT null,
       CLIP BIT null,
       CLIPOVERRIDE BIT null,
       CLIR BIT null,
       COLP BIT null,
       COLPOVERRIDE BIT null,
       COLR BIT null,
       CLIRPRESENTATIONMODE INT null,
       CALLWAITING BIT null,
       CALLHOLD BIT null,
       MULTIPARTY BIT null,
       ECT BIT null,
       CALLWAITING_BSG_1 BIT null,
       CALLWAITING_BSG_6 BIT null,
       CALLWAITING_BSG_7 BIT null,
       CALLWAITING_BSG_8 BIT null,
       CALLWAITING_BSG_12 BIT null,
       UssdAllowed BIT null,
       AOCC BIT null,
       AOCI BIT null,
       TELESERVICELIST NVARCHAR(200) null,
       BEARERSERVICELIST NVARCHAR(200) null,
       ODBMASK NVARCHAR(500) null,
       SERVICECODE NVARCHAR(200) null,
       CREATEDATE DATETIME null,
       USERID INT null,
       CB_SUBSOPTION INT null,
       CB_PASSWORD NVARCHAR(10) null,
       CB_WRONGATTEMPTS INT null,
       POSTPAYOCPLMNTEMPLATE INT null,
       PREPAYOCPLMNTEMPLATE INT null,
       PrepayCsiTypeList NVARCHAR(100) null,
       PostpayCsiTypeList NVARCHAR(100) null,
       NAM INT null,
       FTNRule NVARCHAR(15) null,
       primary key (PROVISIONID)
    )

    create table SYS25_MESSAGE (
        ID BIGINT not null,
       MessageID BIGINT null,
       SourceID BIGINT null,
       LanguageID INT null,
       TypeCode INT null,
       UserMessageCaption NVARCHAR(100) null,
       UserMessageText NVARCHAR(1024) null,
       SystemMessageCaption NVARCHAR(100) null,
       SystemMessageText NVARCHAR(2048) null,
       UpdateDate DATETIME null,
       State INT null,
       primary key (ID)
    )

    create table CRM_MVNO_PROMOTED_TOPUP_HISTORY (
        LOGID BIGINT not null,
       MVNOID INT null,
       CUSTOMERID INT null,
       PROMOTIONID BIGINT null,
       MSISDN NVARCHAR(25) null,
       ORDERCODE NVARCHAR(50) null,
       BEFORECREDIT DECIMAL(19,5) null,
       AFTERCREDIT DECIMAL(19,5) null,
       BEFOREENDDATE DATETIME null,
       AFTERENDDATE DATETIME null,
       OPERATIONDATE DATETIME null,
       primary key (LOGID)
    )

    create table CRM_MVNO_TOPUP_BONUS_DETAIL (
        BONUSDETAILID INT not null,
       BONUSID INT null,
       AMOUNTSTART DECIMAL(19,5) null,
       AMOUNTSTARTINCLUDED BIT null,
       AMOUNTEND DECIMAL(19,5) null,
       AMOUNTENDINCLUDED BIT null,
       FIXED BIT null,
       ENABLED BIT null,
       BONUSAMOUNT DECIMAL(19,5) null,
       PACKAGEID INT null,
       BONUSACCOUNT INT null,
       primary key (BONUSDETAILID)
    )

    create table CRM_MVNO_TOPUP_BONUS_FAILED_PROMOTION (
        ID INT not null,
       DEALERID INT null,
       CUSTOMERID INT null,
       MSISDN NVARCHAR(25) null,
       BONUSCONFIGID INT null,
       PROMOTIONID INT null,
       TOPUPHISTORYID BIGINT null,
       ERRORMESSAGE NVARCHAR(3000) null,
       TOPUPDATE DATETIME null,
       primary key (ID)
    )

    create table CRM_MVNO_TOPUP_BONUS (
        BONUSID INT not null,
       DEALERID INT null,
       NAME NVARCHAR(50) null,
       CHANNEL NVARCHAR(50) null,
       DAYINYEAR NVARCHAR(800) null,
       DAYINYEARENABLED BIT null,
       DAYINMONTH NVARCHAR(50) null,
       DAYINMONTHENABLED BIT null,
       DAYINWEEK NVARCHAR(50) null,
       DAYINWEEKENABLED BIT null,
       DETAILENABLED BIT null,
       PROMOTIONENABLED BIT null,
       ENABLED BIT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       PRIORITY INT null,
       CREATEDATE DATETIME null,
       CUMULATIVE BIT null,
       RecurrenceNumber INT null,
       PROMOTIONRECURRENCENUMBER INT null,
       PROMOTIONBONUSNEEDFEE BIT null,
       primary key (BONUSID)
    )

    create table CRM_MVNO_TOPUP_BONUS_PROMOTION (
        BONUSPROMOTIONID INT not null,
       BONUSID INT null,
       PACKAGEID INT null,
       PROMOTIONID INT null,
       AMOUNTSTART DECIMAL(19,5) null,
       AMOUNTEND DECIMAL(19,5) null,
       ENABLED BIT null,
       RECURRENT BIT null,
       RECURRENCENUMBER INT null,
       AMOUNTSTARTINCLUDED BIT null,
       AMOUNTENDINCLUDED BIT null,
       primary key (BONUSPROMOTIONID)
    )

    create table CRM_MVNO_TOPUPPROMOTION_CONFIG (
        DEALERID INT not null,
       STARTAMOUNT DECIMAL(19,5) not null,
       STARTAMOUNTINCLUDED BIT not null,
       ENDAMOUNT DECIMAL(19,5) not null,
       STARTDATE DATETIME not null,
       ENABLED BIT not null,
       ENDAMOUNTINCLUDED BIT null,
       PROMOTIONID INT null,
       DURATIONUNIT INT null,
       DURATION INT null,
       ENDDATE DATETIME null,
       PRIORITY INT null,
       primary key (DEALERID, STARTAMOUNT, STARTAMOUNTINCLUDED, ENDAMOUNT, STARTDATE, ENABLED)
    )

    create table CRM_CUSTOMERS_CREDITCARDS (
        CUSTOMERID INT not null,
       CARDNUMBER NVARCHAR(20) not null,
       EXPIRATIONDATE DATETIME null,
       ENTITY NVARCHAR(255) null,
       NAMEONCARD NVARCHAR(255) null,
       STATUS INT null,
       primary key (CUSTOMERID, CARDNUMBER)
    )

    create table CRM_CUSTOMERS_DATA_ROAMING_LIMIT (
        ID BIGINT not null,
       CUSTOMERID INT null,
       DATAROAMINGLIMIT DECIMAL(19,5) null,
       CONTINUESUM DECIMAL(19,5) null,
       DATAROAMINGCONSUMPTIONCOUNTER DECIMAL(19,5) null,
       CONTINUEBYSMS BIT null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_DATA_ROAMING_LIMIT_HISTORY (
        HISTORYID BIGINT not null,
       CUSTOMERID INT null,
       DEALERID INT null,
       STATUSID INT null,
       MSISDN NVARCHAR(255) null,
       OLDROAMINGLIMIT DECIMAL(19,5) null,
       CURRENTROAMINGLIMIT DECIMAL(19,5) null,
       OLDCONTINUESUM DECIMAL(19,5) null,
       CURRENTCONTINUESUM DECIMAL(19,5) null,
       ROAMINGLIMITCOUNTER DECIMAL(19,5) null,
       CREATEDATE DATETIME null,
       UPDATEBY INT null,
       HISTORYTYPE INT null,
       MESSAGE NVARCHAR(1000) null,
       primary key (HISTORYID)
    )

    create table CRM_CUSTOMERS_DATA_ROAMING_LIMIT_NOTIFICATION (
        ID BIGINT not null,
       CUSTOMERID INT null,
       MVNONOTIFICATIONID INT null,
       STATUSID INT null,
       ISSENT INT null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS (
        CustomerID INT not null,
       DEALERID INT null,
       PARENTID INT null,
       COMPANY NVARCHAR(200) null,
       CONTACT NVARCHAR(200) null,
       TITLEID INT null,
       GENDERID INT null,
       TELEPHONE NVARCHAR(50) null,
       TELEFAX NVARCHAR(25) null,
       MOBILE NVARCHAR(25) null,
       EMAIL NVARCHAR(200) null,
       CHOC NVARCHAR(100) null,
       VAT NVARCHAR(100) null,
       CREATEDATE DATETIME null,
       USERID INT null,
       BILLINGDATE INT null,
       ACTIVEDDATE DATETIME null,
       INITIALS NVARCHAR(200) null,
       MIDDLENAME NVARCHAR(200) null,
       LASTNAME NVARCHAR(200) null,
       STATUSID INT null,
       ParentControl INT null,
       UPDATESTAMP VARBINARY(MAX) null,
       DATEOFBIRTH DATETIME null,
       FirstName NVARCHAR(200) null,
       LastName2 NVARCHAR(200) null,
       SALESSHOPID NVARCHAR(10) null,
       SALESSELLERID NVARCHAR(10) null,
       CUSTOMER_CATEGORY NVARCHAR(4) null,
       LOCALIZEDNAME NVARCHAR(255) null,
       primary key (CustomerID)
    )

    create table CRM_CUSTOMERS_ADDRESSES (
        CUSTOMERID INT not null,
       USAGE_TYPE INT null,
       ADDRESSID INT null
    )

    create table RM_DATECATEGORY (
        DATECATEGORYID INT not null,
       DATECATEGORYSEQ INT not null,
       DATECATEGORYNAME NVARCHAR(50) null,
       DATECATEGORYTYPEID INT null,
       MONTH INT null,
       DAYOFMONTH NVARCHAR(85) null,
       DAYOFWEEK NVARCHAR(15) null,
       STARTTIME INT null,
       ENDTIME INT null,
       REMARK NVARCHAR(100) null,
       primary key (DATECATEGORYID, DATECATEGORYSEQ)
    )

    create table CRM_MVNO_TOPUP_CONFIG (
        DEALERID INT not null,
       MAXTIMESPERDAY INT null,
       MAXTOTALAMOUNTPERDAY DECIMAL(19,5) null,
       MINAMOUNTPERTIME DECIMAL(19,5) null,
       primary key (DEALERID)
    )

    create table CRM_DEALERS_BANK (
        BANKID INT not null,
       BANKNAME NVARCHAR(200) null,
       BANKNUMBER NVARCHAR(100) null,
       CURRENCYID INT null,
       OWNER NVARCHAR(200) null,
       CITY NVARCHAR(200) null,
       COUNTRYID INT null,
       IBAN NVARCHAR(100) null,
       SWIFT NVARCHAR(100) null,
       ABI NVARCHAR(10) null,
       CAB NVARCHAR(10) null,
       CREATEDATE DATETIME null,
       USERID INT null,
       DealerID INT null,
       primary key (BANKID)
    )

    create table CRM_DEALERS_ICCID (
        DEALERID INT not null,
       RANGESEQ INT not null,
       ICCID1 NVARCHAR(20) null,
       ICCID2 NVARCHAR(20) null,
       TOTAL INT null,
       TOTALUSED INT null,
       RANGEUSED NVARCHAR(255) null,
       RANGEAVAILABLE NVARCHAR(255) null,
       primary key (DEALERID, RANGESEQ)
    )

    create table CRM_DEALERS_IMSI (
        DEALERID INT not null,
       RANGESEQ INT not null,
       IMSI1 NVARCHAR(15) null,
       IMSI2 NVARCHAR(15) null,
       TOTAL INT null,
       TOTALUSED INT null,
       RANGEUSED NVARCHAR(255) null,
       RANGEAVAILABLE NVARCHAR(255) null,
       primary key (DEALERID, RANGESEQ)
    )

    create table CRM_DEALERS (
        DEALERID INT not null,
       PARENTID INT null,
       DEALERNODE NVARCHAR(100) null,
       DEALERTYPEID INT null,
       FISCALUNITID INT null,
       RESELLERID INT null,
       AGENTID INT null,
       SUBAGENTID INT null,
       COMPANY NVARCHAR(200) null,
       CONTACT NVARCHAR(200) null,
       TITLEID INT null,
       GENDERID INT null,
       ADDRESS NVARCHAR(200) null,
       HOUSENO NVARCHAR(50) null,
       ZIPCODE NVARCHAR(50) null,
       CITY NVARCHAR(200) null,
       COUNTRYID INT null,
       TELEPHONE NVARCHAR(50) null,
       TELEFAX NVARCHAR(50) null,
       EMAIL NVARCHAR(100) null,
       CHOC NVARCHAR(100) null,
       VAT NVARCHAR(100) null,
       CREATEDATE DATETIME null,
       USERID INT null,
       CREATEUSER INT null,
       UPDATEUSER INT null,
       UPDATEDATE DATETIME null,
       STATE INT null,
       HIDE INT null,
       MVNOTYPEID INT null,
       EXTERNALDEALERID NVARCHAR(25) null,
       primary key (DEALERID)
    )

    create table CRM_DEALERS_LOYALTY_POINT (
        LOYALTYID INT not null,
       LOYALTYNAME NVARCHAR(200) null,
       PAYMENTTYPEID INT null,
       UNITVALUE INT null,
       LOYALTYPOINT INT null,
       ISACTIVE BIT null,
       USERID INT null,
       CREATEDATE DATETIME null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       DealerID INT null,
       primary key (LOYALTYID)
    )

    create table CRM_DEALERS_PROPERTIES (
        PROPERTYID INT not null,
       INVOICENO INT null,
       STATEMENTNO INT null,
       MIN_INVOICE_AMOUNT DECIMAL(19,5) null,
       MIN_INVOICE_PERIOD INT null,
       LOGOID INT null,
       INVOICE_BASED_ON INT null,
       CREATEDATE DATETIME null,
       USERID INT null,
       MAXPININVALIDTIMES INT null,
       CPSCode NVARCHAR(20) null,
       CountryCode INT null,
       TaxPlanId INT null,
       LanguageId INT null,
       InvoiceDetail BIT null,
       InvoiceDueDate INT null,
       BillingEntity INT null,
       BundleId INT null,
       CreditLimit DECIMAL(19,5) null,
       ProvisionID INT null,
       BILLINGSCENARIOID INT null,
       COMMISSIONGROUP INT null,
       DealerID INT null,
       primary key (PROPERTYID)
    )

    create table CRM_PRS_DEALER_RATEPLAN (
        RatePlanDetailId INT not null,
       DealerId INT null,
       CALLTYPEID INT null,
       TIMECATEGORYID INT null,
       SERVICETYPEID INT null,
       SUBSERVICETYPEID INT null,
       OPERATORID NVARCHAR(9) null,
       CURRENCYID INT null,
       UNITCATEGORYID INT null,
       TARIFFCODE NVARCHAR(15) null,
       TARIFF1 DECIMAL(19,5) null,
       TARIFF2 DECIMAL(19,5) null,
       SETUP1 DECIMAL(19,5) null,
       TARIFF3 DECIMAL(19,5) null,
       TARIFF4 DECIMAL(19,5) null,
       SETUP2 DECIMAL(19,5) null,
       CB DECIMAL(19,5) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       primary key (RatePlanDetailId)
    )

    create table CRM_DEALERS_VOUCHER_MVNO (
        DealerId INT IDENTITY NOT NULL,
       SnCode NVARCHAR(2) null,
       COUNTRYCODE NVARCHAR(8) null,
       primary key (DealerId)
    )

    create table SYS_DICTIONARY (
        ID INT not null,
       DICTIONARYID INT null,
       PARENTID INT null,
       DICTIONARYTYPE INT null,
       DICTIONARYTEXT NVARCHAR(50) null,
       VALUESID NVARCHAR(50) null,
       LEVELID INT null,
       ITEM_POST INT null,
       TOOLTIPTEXT NVARCHAR(50) null,
       LINK NVARCHAR(100) null,
       DESCRIPTIONID NVARCHAR(200) null,
       UPDATE_USERID INT null,
       UPDATE_TIME DATETIME null,
       CREATE_USERID INT null,
       CREATE_TIME DATETIME null,
       STATEID INT null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_DID (
        RESOURCEID INT not null,
       DIDNUMBER NVARCHAR(25) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       primary key (RESOURCEID)
    )

    create table CRM_DRE_OPERATION_LOG (
        OperationID BIGINT not null,
       REQUESTID NVARCHAR(100) null,
       USERID INT null,
       DealerID INT null,
       DESCRIPTION NVARCHAR(200) null,
       SHORTCODE NVARCHAR(4) null,
       REMARK NVARCHAR(2000) null,
       OPERATIONDATE DATETIME null,
       RESULTTYPEID INT null,
       UserMessages NVARCHAR(255) null,
       SystemMessages NVARCHAR(255) null,
       NAME1 NVARCHAR(100) null,
       NAME2 NVARCHAR(100) null,
       NAME3 NVARCHAR(100) null,
       NAME4 NVARCHAR(100) null,
       NAME5 NVARCHAR(100) null,
       NAME6 NVARCHAR(100) null,
       NAME7 NVARCHAR(100) null,
       NAME8 NVARCHAR(100) null,
       NAME9 NVARCHAR(100) null,
       NAME10 NVARCHAR(100) null,
       VALUE1 NVARCHAR(100) null,
       VALUE2 NVARCHAR(100) null,
       VALUE3 NVARCHAR(100) null,
       VALUE4 NVARCHAR(100) null,
       VALUE5 NVARCHAR(100) null,
       VALUE6 NVARCHAR(100) null,
       VALUE7 NVARCHAR(100) null,
       VALUE8 NVARCHAR(100) null,
       VALUE9 NVARCHAR(100) null,
       VALUE10 NVARCHAR(100) null,
       primary key (OperationID)
    )

    create table CRM_EMAIL_ATTACHMENT_HISTORY (
        ATTACHMENTID BIGINT not null,
       REMARK NVARCHAR(600) null,
       FILENAME NVARCHAR(200) null,
       FILEPATH NVARCHAR(400) null,
       FILECONTENT VARBINARY(MAX) null,
       EmailId BIGINT null,
       primary key (ATTACHMENTID)
    )

    create table CRM_EMAIL_HISTORY (
        EMAILID BIGINT not null,
       DEALERID INT null,
       CATEGORYID INT null,
       SENDER NVARCHAR(200) null,
       EMAILFROM NVARCHAR(200) null,
       EMAILTO NVARCHAR(600) null,
       CC NVARCHAR(600) null,
       BCC NVARCHAR(600) null,
       EMAILSUBJECT NVARCHAR(400) null,
       SUBJECTENCODING NVARCHAR(200) null,
       BODY NVARCHAR(255) null,
       BODYENCODING NVARCHAR(200) null,
       ISBODYHTML BIT null,
       REPLYTO NVARCHAR(200) null,
       PRIORITY INT null,
       REMARK NVARCHAR(600) null,
       CREATEDATE DATETIME null,
       STATUSID INT null,
       SENTDATE DATETIME null,
       USERID INT null,
       UserName NVARCHAR(200) null,
       UserPassword NVARCHAR(200) null,
       SmtpHost NVARCHAR(200) null,
       TRYSENTTIMES INT null,
       MAXSENTTIMES INT null,
       CUSTOMERID INT null,
       primary key (EMAILID)
    )

    create table CRM_LIFECYCLE_EVENTS (
        EVENTID INT not null,
       EVENTCODE NVARCHAR(20) null,
       EVENTNAME NVARCHAR(510) null,
       EVENTTYPE INT null,
       OPERATETYPE NVARCHAR(1) null,
       primary key (EVENTID)
    )

    create table CRM_EXECUTE_TRACE (
        ID BIGINT not null,
       TYPEID INT null,
       ASSEMBLYNAME NVARCHAR(200) null,
       CLASSNAME NVARCHAR(200) null,
       METHODNAME NVARCHAR(200) null,
       INPUTPARAM VARBINARY(MAX) null,
       BEGINDATE DATETIME null,
       ENDDATE DATETIME null,
       DATASIZE NVARCHAR(500) null,
       PRECPULOAD NVARCHAR(100) null,
       PREMEMORYSTATUS NVARCHAR(100) null,
       POSTCPULOAD NVARCHAR(100) null,
       POSTMEMORYSTATUS NVARCHAR(100) null,
       TRACEORDER NVARCHAR(50) null,
       DESCRIPTION NVARCHAR(500) null,
       STEP INT null,
       primary key (ID)
    )

    create table CRM_MVNO_EXTERNAL_RESOURCE (
        RESOURCEID INT not null,
       DEALERID INT null,
       RESOURCE NVARCHAR(25) null,
       STATUSID INT null,
       CREATEDATE DATETIME null,
       REMARK NVARCHAR(200) null,
       primary key (RESOURCEID)
    )

    create table CRM_MVNO_TOPUP_HISTORY (
        ID BIGINT not null,
       DealerID INT null,
       OPERATIONCODE BIGINT null,
       REFERENCECODE NVARCHAR(50) null,
       MSISDN NVARCHAR(25) null,
       TOPUPTYPE INT null,
       PIN NVARCHAR(40) null,
       STATUS INT null,
       STATUSDATE DATETIME null,
       CURRENCYID INT null,
       TAXCODE INT null,
       PREOPERATIONCODE BIGINT null,
       PREBALANCE DECIMAL(19,5) null,
       OPERATIONCAUSE NVARCHAR(20) null,
       REMARK NVARCHAR(1000) null,
       Used BIT null,
       UsedTime DATETIME null,
       PAYMENTCARDNUMBER NVARCHAR(25) null,
       EXPIRATIONDATE NVARCHAR(4) null,
       AMOUNTWITHTAX DECIMAL(19,5) null,
       AMOUNT DECIMAL(19,5) null,
       BONUSCONFIGIDS NVARCHAR(200) null,
       BONUSCONFIGNAME NVARCHAR(1000) null,
       TOPUPAMOUNT DECIMAL(19,5) null,
       BONUSAMOUNTWITHTAX DECIMAL(19,5) null,
       BONUSAMOUNT DECIMAL(19,5) null,
       NORMALAMOUNTWITHTAX DECIMAL(19,5) null,
       NORMALAMOUNT DECIMAL(19,5) null,
       TOPUPBONUSWITHTAX DECIMAL(19,5) null,
       TOPUPBONUS DECIMAL(19,5) null,
       BONUSFORNORMAL DECIMAL(19,5) null,
       BONUSFORNORMALVAT DECIMAL(19,5) null,
       BONUSFORPROMOTED DECIMAL(19,5) null,
       BONUSFORPROMOTEDVAT DECIMAL(19,5) null,
       BONUSPROMOTIONIDS NVARCHAR(200) null,
       BONUSPROMOTIONNAME NVARCHAR(1000) null,
       UserID INT null,
       CustomerID INT null,
       VCENCRYPT NVARCHAR(50) null,
       TopupAccount INT null,
       WALLET_BALANCE_ADJUSTMENT_CODE INT null,
       EXPIRATIONDATE2 DATETIME null,
       ValidityDays INT null,
       primary key (ID)
    )

    create table CRM_HOLIDAY (
        INTDATE INT not null,
       ISHOLIDAY TINYINT null,
       HOLIDAYDESCRIPTION NVARCHAR(100) null,
       primary key (INTDATE)
    )

    create table CRM_RESMB_HUNTING_GROUP_DEST (
        DESTINATIONID INT not null,
       GROUPID INT null,
       DESTINATION NVARCHAR(25) null,
       PRIORITY INT null,
       CREATEDATE DATETIME null,
       REMARK NVARCHAR(200) null,
       primary key (DESTINATIONID)
    )

    create table CRM_RESMB_HUNTING_GROUP (
        GROUPID INT not null,
       RESOURCEID INT null,
       NOREPLYTIMER1 INT null,
       NOREPLYTIMER2 INT null,
       MAXJUMPS INT null,
       CREATEDATE DATETIME null,
       REMARK NVARCHAR(200) null,
       primary key (GROUPID)
    )

    create table CRM_MVNO_HUNTING_GROUP_SETTING (
        SETTINGID INT not null,
       DEALERID INT null,
       NOREPLYTIMER1 INT null,
       NOREPLYTIMER2 INT null,
       MAXJUMPS INT null,
       CREATEDATE DATETIME null,
       REMARK NVARCHAR(200) null,
       primary key (SETTINGID)
    )

    create table CRM_MVNO_ID_MAPPING (
        ID BIGINT IDENTITY NOT NULL,
       MvnoId INT null,
       ETID1 NVARCHAR(50) null,
       ETID2 NVARCHAR(50) null,
       ETID3 NVARCHAR(50) null,
       ExternalID1 NVARCHAR(50) null,
       ExternalID2 NVARCHAR(50) null,
       MappingType INT null,
       primary key (ID)
    )

    create table MSISDN_IMEI_ASSN (
        RESOURCEID INT not null,
       IMEI NVARCHAR(255) null,
       STARTDATE DATETIME null,
       primary key (RESOURCEID)
    )

    create table MSISDN_IMEI_ASSN_HIST (
        STARTDATE DATETIME not null,
       RESOURCEID INT not null,
       IMEI NVARCHAR(255) null,
       ENDDATE DATETIME null,
       primary key (STARTDATE, RESOURCEID)
    )

    create table CRM_MOBILE_SIMCARDS_IMPORT_DETAILS (
        TRANSACTIONID NVARCHAR(36) not null,
       ICCID NVARCHAR(20) not null,
       IMSI NVARCHAR(32) null,
       PIN1 NVARCHAR(4) null,
       PIN2 NVARCHAR(4) null,
       PUK1 NVARCHAR(10) null,
       PUK2 NVARCHAR(10) null,
       KI NVARCHAR(200) null,
       OPC NVARCHAR(32) null,
       KIC_0F NVARCHAR(32) null,
       KID_0F NVARCHAR(32) null,
       KIK_0F NVARCHAR(32) null,
       ADM1 NVARCHAR(16) null,
       ADM2 NVARCHAR(16) null,
       ACC NVARCHAR(4) null,
       MSISDN NVARCHAR(25) null,
       FLAG BIT null,
       IMSI2 NVARCHAR(32) null,
       IMSI3 NVARCHAR(32) null,
       IMSI4 NVARCHAR(32) null,
       IMSI5 NVARCHAR(32) null,
       IMSI6 NVARCHAR(32) null,
       IMSI7 NVARCHAR(32) null,
       IMSI8 NVARCHAR(32) null,
       IMSI9 NVARCHAR(32) null,
       IMSI10 NVARCHAR(32) null,
       IMSI11 NVARCHAR(32) null,
       IMSI12 NVARCHAR(32) null,
       IMSI13 NVARCHAR(32) null,
       IMSI14 NVARCHAR(32) null,
       IMSI15 NVARCHAR(32) null,
       primary key (TRANSACTIONID, ICCID)
    )

    create table CRM_MOBILE_SIMCARDS_IMPORT_LOG (
        ID INT IDENTITY NOT NULL,
       TRANSACTIONID NVARCHAR(36) null,
       ICCID NVARCHAR(20) null,
       RESOURCE NVARCHAR(25) null,
       IMSI1 NVARCHAR(15) null,
       DEALERID INT null,
       STARTTIME DATETIME null,
       ENDTIME DATETIME null,
       RESULTCODE INT null,
       DESCRIPTION NVARCHAR(510) null,
       USERID INT null,
       primary key (ID)
    )

    create table CRM_MOBILE_SIMCARDS_IMPORT_MASTER (
        TRANSACTIONID NVARCHAR(255) not null,
       ORDERID INT null,
       ALGORITHMNAME NVARCHAR(32) null,
       MANUFACTURERID NVARCHAR(32) null,
       LANGUAGEID INT null,
       INIPASSWORD_DES NVARCHAR(50) null,
       INIPASSWORD_MD5 NVARCHAR(32) null,
       PACKAGEID INT null,
       CREDITLIMIT DECIMAL(19,5) null,
       ACTIVATETYPE INT null,
       USERID INT null,
       STARTDATETIME DATETIME null,
       DEALERID INT null,
       DETAILSCOUNT INT null,
       IMPORTDATE DATETIME null,
       primary key (TRANSACTIONID)
    )

    create table RM_PRS_INCUMBENT (
        ID INT not null,
       CUSTOMERID INT null,
       SERVICETYPEID INT null,
       SUBSERVICETYPEID INT null,
       TypeId INT null,
       TARIFFCODE NVARCHAR(8) null,
       OPERATORID NVARCHAR(40) null,
       CARRIER NVARCHAR(50) null,
       CALLTYPEID INT null,
       TIMECATEGORYID INT null,
       UNITCATEGORYID1 INT null,
       UNITCATEGORYID2 INT null,
       TARIFF1 DECIMAL(19,5) null,
       TARIFF2 DECIMAL(19,5) null,
       TARIFF3 DECIMAL(19,5) null,
       TARIFF4 DECIMAL(19,5) null,
       SETUP1 DECIMAL(19,5) null,
       SETUP2 DECIMAL(19,5) null,
       CB DECIMAL(19,5) null,
       CURRENCYID INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       primary key (ID)
    )

    create table CRM_INTERFACE_HISTORY (
        SEQID BIGINT not null,
       REFERENCECODE NVARCHAR(255) not null,
       REQUESTXML NVARCHAR(255) not null,
       RESPONSEXML NVARCHAR(255) null,
       CREATETIME DATETIME not null,
       primary key (SEQID)
    )

    create table RM_INTERNATIONAL_ZONE_DETAILS (
        ZONEDETAILID INT not null,
       COUNTRYCODE NVARCHAR(25) null,
       COUNTRYNAME NVARCHAR(400) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       ZoneID INT null,
       primary key (ZONEDETAILID)
    )

    create table RM_INTERNATIONAL_ZONE (
        ZONEID INT not null,
       ZONENAME NVARCHAR(400) null,
       DEALERID INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       primary key (ZONEID)
    )

    create table RM_ROAMING_GSNIP_RANGE (
        RANGEID INT not null,
       TSC NVARCHAR(20) null,
       IPRANGE1 BIGINT null,
       IPRANGE2 BIGINT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       primary key (RANGEID)
    )

    create table SYS_LANGUAGETYPE (
        ID INT IDENTITY NOT NULL,
       LanguageID INT null,
       LanguageName NVARCHAR(100) null,
       UpdateDate DATETIME null,
       State INT null,
       CultureName NVARCHAR(100) null,
       CultureIdentifier INT null,
       LanguageCountryRegion NVARCHAR(150) null,
       ShortCode NVARCHAR(20) null,
       primary key (ID)
    )

    create table SYS25_LOG_LEVEL (
        ID INT not null,
       DealerID INT null,
       Business NVARCHAR(100) null,
       ParentID INT null,
       Operation NVARCHAR(100) null,
       OperationLevel INT null,
       RecordLocation INT null,
       CreateDate DATETIME null,
       CreateUserID INT null,
       Status INT null,
       UpdateDate DATETIME null,
       UpdateUserID INT null,
       CLASSNAME NVARCHAR(255) null,
       primary key (ID)
    )

    create table CRM_LIFECYCLE_HOLIDAY (
        MVNOID INT not null,
       INTDATE INT not null,
       HOLIDAYDESCRIPTION NVARCHAR(200) null,
       primary key (MVNOID, INTDATE)
    )

    create table MVNO_FC_LIFECYCLE_LOG (
        Logid BIGINT not null,
       TransactionTime NVARCHAR(14) null,
       TransactionSource NVARCHAR(4) null,
       TransactionType NVARCHAR(2) null,
       MSISDN NVARCHAR(60) null,
       DealerId INT null,
       PackageName NVARCHAR(200) null,
       Language NVARCHAR(16) null,
       CreateTime NVARCHAR(14) null,
       Status NVARCHAR(16) null,
       RechargeTime NVARCHAR(14) null,
       ExpireTime NVARCHAR(14) null,
       Balance DECIMAL(19,5) null,
       Status2 NVARCHAR(16) null,
       RechargeTime2 NVARCHAR(14) null,
       ExpireTime2 NVARCHAR(14) null,
       Balance2 DECIMAL(19,5) null,
       Channel NVARCHAR(4) null,
       PIN NVARCHAR(160) null,
       UserId INT null,
       Used BIT null,
       UsedTime DATETIME null,
       ACTIVEDEADLINEDATE DATETIME null,
       ORDERCODE BIGINT null,
       CHANGESTATUSDATE DATETIME null,
       PAYMENTTYPEID INT null,
       LASTCONSUMEEFFECTIVEDATE DATETIME null,
       primary key (Logid)
    )

    create table MVNO_FC_LIFECYCLE_LOG_EXTEND (
        ExtendId BIGINT not null,
       TopUpType INT null,
       NormalChargeTime NVARCHAR(255) null,
       NormalChargeTime2 NVARCHAR(255) null,
       LOGID BIGINT null,
       primary key (ExtendId)
    )

    create table CRM_LIFECYCLE_STATUS_TRANSITION_NOTIFICATION (
        SEQID BIGINT not null,
       MSISDN NVARCHAR(40) not null,
       PRESTATUS INT null,
       CURRENTSTATUS INT null,
       EVENTID INT not null,
       STATUSID INT not null,
       MVNOID INT not null,
       CREATETIME DATETIME not null,
       UPDATETIME DATETIME null,
       primary key (SEQID)
    )

    create table SYS25_USER (
        UserID INT not null,
       TitleID INT null,
       FirstName NVARCHAR(100) null,
       MiddleName NVARCHAR(100) null,
       LastName NVARCHAR(100) null,
       Company NVARCHAR(100) null,
       LanguageID INT null,
       DepartmentID INT null,
       StatusID INT null,
       UserName NVARCHAR(50) null,
       Password NVARCHAR(100) null,
       Telephone NVARCHAR(30) null,
       Mobile NVARCHAR(30) null,
       Email NVARCHAR(100) null,
       CreateDate DATETIME null,
       CreateUserID INT null,
       Status INT null,
       UpdateDate DATETIME null,
       UpdateUserID INT null,
       FiscalUnit INT null,
       LogoID INT null,
       AccountStatus INT null,
       ACCOUNT_EXPIRE INT null,
       ACCOUNT_CHANGE INT null,
       ACCOUNT_FIRST DATETIME null,
       ACCOUNT_LAST DATETIME null,
       ACCOUNT_GUI DATETIME null,
       ACCOUNT_PASSWORD DATETIME null,
       SHOWALLDEALER INT null,
       StyleID INT null,
       ThemeID INT null,
       ACCOUNT_CHANGE_ALERTING INT null,
       ACCOUNT_PASSWORD_PERIOD INT null,
       USERTYPE INT null,
       primary key (UserID)
    )

    create table CRM_CUSTOMERS_MAPPING (
        MAPPINGID INT not null,
       FISCALUNITID INT null,
       RESELLERID INT null,
       AGENTID INT null,
       SUBAGENTID INT null,
       ORGID INT null,
       OLDID INT null,
       STAT1 BIT null,
       STAT2 BIT null,
       STAT3 BIT null,
       STAT4 BIT null,
       STAT5 BIT null,
       STAT6 BIT null,
       STAT7 BIT null,
       CUSTOMERID INT null,
       primary key (MAPPINGID)
    )

    create table SYS_MESSAGE (
        MESSAGEID INT IDENTITY NOT NULL,
       MESSAGETEXT NVARCHAR(510) null,
       CREATEDATE DATETIME null,
       STARTDATE DATETIME null,
       EXPIREMINUTE INT null,
       USERNAME NVARCHAR(100) null,
       USERID INT null,
       primary key (MESSAGEID)
    )

    create table SYS_MESSAGE_USER (
        ID INT not null,
       USERID INT null,
       MESSAGEID INT null,
       STATUS INT null,
       SENDDATE DATETIME null,
       primary key (ID)
    )

    create table CRM_MNP_CONSOLIDATION_LOG (
        FILENAME NVARCHAR(255) not null,
       OPERATORCODE NVARCHAR(10) null,
       USERID INT null,
       FILETYPE NVARCHAR(2) null,
       RELATEDDATE DATETIME null,
       TURN NVARCHAR(1) null,
       OPERATEDATE DATETIME null,
       STATUS INT null,
       primary key (FILENAME)
    )

    create table CRM_LIFECYCLE_MNP_DELETION_REQUEST (
        REQUESTID INT not null,
       MSISDN NVARCHAR(25) null,
       REQUESTDATE DATETIME null,
       ISSENT BIT null,
       SENDDATE DATETIME null,
       USERID INT null,
       primary key (REQUESTID)
    )

    create table CRM_MNP_INCOMING_EFFECT (
        SEQID BIGINT not null,
       CUSTOMERID INT null,
       TEMPNO NVARCHAR(25) null,
       PORTEDNO NVARCHAR(25) null,
       EFFECTDATE DATETIME null,
       STATUS INT null,
       INCOMINGTYPE INT null,
       CREATEDATE DATETIME null,
       primary key (SEQID)
    )

    create table CRM_MNP_NPDB_ESVF (
        SeqID BIGINT not null,
       MSISDN NVARCHAR(25) null,
       OPERATORCODE NVARCHAR(10) null,
       NRN NVARCHAR(50) null,
       EFFECTDATE DATETIME null,
       CREATEDATE DATETIME null,
       OWNEROPERATOR NVARCHAR(10) null,
       primary key (SeqID)
    )

    create table CRM_MNP_ORIGINAL_NRN (
        SeqID BIGINT not null,
       RANGE1 NVARCHAR(25) null,
       RANGE2 NVARCHAR(25) null,
       OPERATORCODE NVARCHAR(10) null,
       NRN NVARCHAR(10) null,
       OPERATORNAME NVARCHAR(400) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CREATEDATE DATETIME null,
       primary key (SeqID)
    )

    create table CRM_MNP_OPERATOR_NRN_RANGE (
        SeqID BIGINT not null,
       RANGE1 NVARCHAR(10) null,
       RANGE2 NVARCHAR(10) null,
       OPERATORCODE NVARCHAR(10) null,
       OPERATORNAME NVARCHAR(400) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CREATEDATE DATETIME null,
       primary key (SeqID)
    )

    create table CRM_MNP_PORTABILITY_CUSTOMER (
        CUSTOMERID INT not null,
       REFERENCECODE NVARCHAR(25) null,
       CREATEDATE DATETIME null,
       primary key (CUSTOMERID)
    )

    create table CRM_MNP_PORTABILITY (
        REFERENCECODE NVARCHAR(25) not null,
       TYPE NVARCHAR(1) null,
       IDENTIFICATIONTYPE NVARCHAR(1) null,
       IDENTIFICATION NVARCHAR(50) null,
       FIRSTNAME NVARCHAR(50) null,
       LASTNAME1 NVARCHAR(50) null,
       LASTNAME2 NVARCHAR(50) null,
       COMPANYNAME NVARCHAR(200) null,
       COUNTRYCODE INT null,
       PTYPE NVARCHAR(1) null,
       DONATIONOPERATOR NVARCHAR(10) null,
       RECEPTIONOPERATOR NVARCHAR(10) null,
       SRFC NVARCHAR(50) null,
       RECEIVERNRN NVARCHAR(50) null,
       REQUESTDATE DATETIME null,
       RCW DATETIME null,
       EFFECTDATE DATETIME null,
       MSISDN NVARCHAR(25) null,
       ICCID NVARCHAR(40) null,
       STATUS NVARCHAR(1) null,
       USERID INT null,
       PORTABILITYDIRECTION INT null,
       DEALERID INT null,
       REALREFCODE NVARCHAR(50) null,
       ReasonCode NVARCHAR(20) null,
       CreateDate DATETIME null,
       DeleteDate DATETIME null,
       ChageStatusDate DATETIME null,
       ReadDate DATETIME null,
       CancellationCode NVARCHAR(20) null,
       ProductPurchasedId NVARCHAR(MAX) null,
       primary key (REFERENCECODE)
    )

    create table CRM_MNP_PORTABILITY_MULTI (
        SeqID BIGINT not null,
       REFERENCECODE NVARCHAR(25) null,
       MSISDN NVARCHAR(25) null,
       ICC NVARCHAR(25) null,
       STATUS INT null,
       primary key (SEQID)
    )

    create table CRM_MNP_PORTABILITY_ATTACH (
        REFERENCECODE NVARCHAR(25) not null,
       ATTACHNAME NVARCHAR(100) null,
       ATTACHSTREAM VARBINARY(MAX) null,
       SAVEDATE DATETIME null,
       STATUS INT null,
       primary key (REFERENCECODE)
    )

    create table CRM_MNP_REASON (
        REASONID INT not null,
       REASONCODE NVARCHAR(20) null,
       REASONDESCRIPTION NVARCHAR(100) null,
       REASONTYPE INT null,
       LanguageID INT null,
       primary key (REASONID)
    )

    create table CRM_MNP_SessionInfo (
        ID INT not null,
       SESSIONID NVARCHAR(200) null,
       OPERATORCODE NVARCHAR(50) null,
       CREATETIME DATETIME null,
       USERNAME NVARCHAR(20) null,
       primary key (ID)
    )

    create table SYS25_MODULE (
        ModuleID INT not null,
       ParentID INT null,
       ModuleName NVARCHAR(100) null,
       ModuleForm NVARCHAR(100) null,
       ModuleFullPath NVARCHAR(512) null,
       ModuleLevel INT null,
       Remark NVARCHAR(2000) null,
       CreateDate DATETIME null,
       CreateUserID INT null,
       Status INT null,
       UpdateDate DATETIME null,
       UpdateUserID INT null,
       SHOWORDER INT null,
       primary key (ModuleID)
    )

    create table SYS25_MODULE_ELEMENTS (
        ID INT not null,
       SHOWMODULEID INT null,
       FULLCLASSNAME NVARCHAR(510) null,
       ELEMENTID NVARCHAR(510) null,
       ELEMENTTYPE INT null,
       primary key (ID)
    )

    create table SYS25_MODULE_MVNO (
        ID INT not null,
       SHOWMODULEID INT null,
       PARENTID INT null,
       MODULELEVEL INT null,
       MODULENAME NVARCHAR(100) null,
       MODULEFULLNAME NVARCHAR(510) null,
       MVNOID INT null,
       CREATEUSER INT null,
       CREATEDATE DATETIME null,
       primary key (ID)
    )

    create table SYS_MULTILINGUAL (
        ID INT not null,
       DealerID INT null,
       LanguageID INT null,
       FormFullName NVARCHAR(510) null,
       ControlID NVARCHAR(250) null,
       Value NVARCHAR(510) null,
       Text NVARCHAR(510) null,
       ToolTipText NVARCHAR(510) null,
       ShowOrder INT null,
       Link NVARCHAR(510) null,
       Description NVARCHAR(510) null,
       UpdateDate DATETIME null,
       State INT null,
       DictionaryTypeID INT null,
       REGEXTYPEID INT null,
       DEFAULTTEXT NVARCHAR(510) null,
       ControlType NVARCHAR(50) null,
       ShowType INT null,
       primary key (ID)
    )

    create table CRM_MVNO_APN_IP_BATCH_INFO (
        BATCHID INT not null,
       APN NVARCHAR(200) null,
       DealerId NVARCHAR(600) null,
       MVNOID INT null,
       COUNT INT null,
       ShareType INT null,
       UserID INT null,
       CreateDate DATETIME null,
       IPStart NVARCHAR(25) null,
       IPEnd NVARCHAR(25) null,
       primary key (BATCHID)
    )

    create table CRM_MVNO_APN_IP_POOL (
        ID INT not null,
       BATCHID INT null,
       APN NVARCHAR(200) null,
       MSISDN NVARCHAR(25) null,
       IPADDRESS NVARCHAR(50) null,
       STATUS INT null,
       UPDATEUSER INT null,
       LASTMODIFY DATETIME null,
       primary key (ID)
    )

    create table CRM_MVNO_AUTO_TOPUP_NOTIFICATION_LOG (
        NOTIFICATIONID BIGINT not null,
       DEALERID INT null,
       MSISDN NVARCHAR(25) null,
       CUSTOMERID INT null,
       THRESHOLD DECIMAL(19,5) null,
       CURRENTLIMIT DECIMAL(19,5) null,
       TOPUPAMOUNT DECIMAL(19,5) null,
       CREATEDATE DATETIME null,
       STATUS TINYINT null,
       EXECUTECOUNT TINYINT null,
       NOTIFICATIONFROM TINYINT null,
       OPERATIONDATE DATETIME null,
       primary key (NOTIFICATIONID)
    )

    create table CRM_MVNO_AUTO_TOPUP_NOTIFICATION_PROVISION_LOG (
        ID BIGINT IDENTITY NOT NULL,
       NOTIFICATIONID BIGINT null,
       DEALERID INT null,
       MSISDN NVARCHAR(25) null,
       REQUESTXML NVARCHAR(3000) null,
       RESPONSEXML NVARCHAR(3000) null,
       NOTIFICATIONFROM TINYINT null,
       CREATEDATE DATETIME null,
       STATUS TINYINT null,
       primary key (ID)
    )

    create table CRM_MVNO_AUTO_TOPUP_NOTIFICATION_QUEUE (
        NOTIFICATIONID BIGINT IDENTITY NOT NULL,
       DEALERID INT null,
       MSISDN NVARCHAR(25) null,
       CUSTOMERID INT null,
       THRESHOLD DECIMAL(19,5) null,
       CURRENTLIMIT DECIMAL(19,5) null,
       TOPUPAMOUNT DECIMAL(19,5) null,
       SENDTIMES TINYINT null,
       LASTSENDTIME DATETIME null,
       NOTIFICATIONFROM TINYINT null,
       CREATEDATE DATETIME null,
       primary key (NOTIFICATIONID)
    )

    create table CRM_MVNO_CONFIGACTION (
        ID INT not null,
       CATEGORYID INT null,
       CATEGORYNAME NVARCHAR(100) null,
       ITEM NVARCHAR(100) null,
       NAME NVARCHAR(100) null,
       VALUE NVARCHAR(200) null,
       DESCRIPTION NVARCHAR(200) null,
       STATUSID INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CREATEDATE DATETIME null,
       BAK1 NVARCHAR(100) null,
       BAK2 NVARCHAR(100) null,
       BAK3 NVARCHAR(100) null,
       BAK4 NVARCHAR(100) null,
       BAK5 NVARCHAR(100) null,
       DealerID INT null,
       primary key (ID)
    )

    create table CRM_MVNO_CUSTOMERS_PROPERTY (
        PROPERTYID INT not null,
       NATIONALITY INT null,
       JOBTITLE INT null,
       OTHERJOBTITLE NVARCHAR(200) null,
       DELIVERYADDRESS NVARCHAR(255) null,
       IMEI NVARCHAR(50) null,
       ADVERTISEMENTBARRING INT null,
       RECURRINGTOPUPSTATUS INT null,
       RECURRINGTOPUPDATE DATETIME null,
       RECURRINGTOPUPAMOUNT DECIMAL(19,5) null,
       MINIMUMTOPUPSTATUS INT null,
       MINIMUMAMOUNT DECIMAL(19,5) null,
       MINIMUMTOPUPAMOUNT DECIMAL(19,5) null,
       UPDATEDONLINE BIT null,
       UPDATEDONLINEDATE DATETIME null,
       FIRSTLOGINTIME DATETIME null,
       REGISTERDATE DATETIME null,
       ADDEDDATE DATETIME null,
       FirstRoamingTime DATETIME null,
       BENCUSTOMERTYPE INT null,
       ENABLECELLID INT null,
       TEAMID NVARCHAR(20) null,
       HANDSET NVARCHAR(25) null,
       GOALALERTINGSTATUS INT null,
       GOALUPDATESTATUS INT null,
       GOALCREDIT INT null,
       CUSTOMERAGENTTYPEID INT null,
       AUTOTOPUPSTATUS INT null,
       AUTOTOPUPAMOUNT DECIMAL(19,5) null,
       AUTOTOPUPTHRESHOLD DECIMAL(19,5) null,
       AUTOTOPUPSENDFLAG INT null,
       CUSTOMERID INT null,
       primary key (PROPERTYID)
    )

    create table CRM_MVNO_DATA_ROAMING_LIMIT (
        ID INT not null,
       ZONEID INT null,
       APN NVARCHAR(1000) null,
       PREPAYCONSUMPTIONLIMIT DECIMAL(19,5) null,
       PREPAYCONSUMPTIONCONTINUELIMIT DECIMAL(19,5) null,
       PREPAYLIMITUNIT INT null,
       POSTPAYCONSUMPTIONLIMIT DECIMAL(19,5) null,
       POSTPAYCONSUMPTIONCONTINUELIMIT DECIMAL(19,5) null,
       POSTPAYLIMITUNIT INT null,
       DEFAULTAPPLICATIONFLAG BIT null,
       UPDATEDATE DATETIME null,
       CONTINUEBYSMS BIT null,
       MVNOID INT null,
       primary key (ID)
    )

    create table CRM_MVNO_DATA_ROAMING_LIMIT_NOTIFICATION (
        ID INT not null,
       MVNOID INT null,
       NotificationType INT null,
       StatusId INT null,
       ThresholdLimit DECIMAL(19,5) null,
       TemplateId INT null,
       LimitUnit INT null,
       SMSType INT null,
       ThresholdSubType INT null,
       UpdateDate DATETIME null,
       primary key (ID)
    )

    create table SYS_MVNO_LANGUAGE (
        ID INT not null,
       MVNOID INT null,
       LanguageID INT null,
       primary key (ID)
    )

    create table CRM_MVNO_NOTIFICATION_SETTINGS (
        ID INT not null,
       MVNOID INT null,
       NOTIFICATION_TYPE INT null,
       TEMPLATEID INT null,
       StatusID INT null,
       VALUE1 NVARCHAR(255) null,
       VALUE2 NVARCHAR(255) null,
       VALUE3 NVARCHAR(255) null,
       VALUE4 NVARCHAR(255) null,
       VALUE5 NVARCHAR(255) null,
       VALUE6 NVARCHAR(255) null,
       primary key (ID)
    )

    create table CRM_MVNO_PROMOTION_SMS_CONFIG (
        ID INT not null,
       DEALERID INT null,
       CONFIGID INT null,
       CONFIGNAME NVARCHAR(200) null,
       SMSTYPE INT null,
       STATUS INT null,
       TEMPLATEID INT null,
       LIMIT INT null,
       LIMITUNIT INT null,
       PROMOTIONPLANID INT null,
       PROMOTIONPLANDETAILID INT null,
       primary key (ID)
    )

    create table CRM_MVNO_PROPERTIES (
        DEALERID INT not null,
       VMO NVARCHAR(10) null,
       BRSOPERATORCODE INT null,
       OPERATORCODE NVARCHAR(10) null,
       FF BIT null,
       REQUESTFORM NVARCHAR(50) null,
       PREPAIDBILLINGDATE INT null,
       POSTPAIDBILLINGDATE INT null,
       SMSRegularTime NVARCHAR(10) null,
       SecurityNumberCount INT null,
       QUERYWITHVAT BIT null,
       AUTOTOPUPSCANFLAGFORDRE BIT null,
       AUTOTOPUPSCANFLAGFORCRM BIT null,
       ROAMINGSPENDINGTHRESHOLD DECIMAL(19,5) null,
       ROAMINGSPENDINGNOTIFICATIONSTATUS BIT null,
       SPENDINGTHRESHOLD DECIMAL(19,5) null,
       SPENDINGNOTIFICATIONSTATUS BIT null,
       primary key (DEALERID)
    )

    create table CRM_MVNO_SMS_ACCOUNT (
        AccountId INT not null,
       AppId NVARCHAR(60) null,
       AppUserName NVARCHAR(200) null,
       AppUserPassword NVARCHAR(100) null,
       AppHost NVARCHAR(100) null,
       AppPath NVARCHAR(600) null,
       AppPort NVARCHAR(100) null,
       AppName NVARCHAR(200) null,
       AppWord NVARCHAR(100) null,
       Sender NVARCHAR(100) null,
       DlrUrl NVARCHAR(400) null,
       DealerId INT null,
       primary key (AccountId)
    )

    create table RM_BUNDLE_PACKAGE_TOPUP_BASED (
        PACKAGEID INT not null,
       BUNDLEID INT not null,
       THRESHOLD_TOPUP_AMOUNT DECIMAL(19,5) not null,
       DAYS_VALID INT null,
       DAYS_FROZEN_AFTER_EXPIRATION INT null,
       DAYS_TO_ACCUMULATE_TOPUPS INT null,
       primary key (PACKAGEID, BUNDLEID, THRESHOLD_TOPUP_AMOUNT)
    )

    create table SYS_NPM_BATCH_INFO (
        BatchID INT not null,
       SNStart NVARCHAR(80) null,
       SNEnd NVARCHAR(80) null,
       Count INT null,
       ShareType INT null,
       DealerID NVARCHAR(600) null,
       CreateDate DATETIME null,
       UserID INT null,
       primary key (BatchID)
    )

    create table SYS_NPM_HISTORY (
        Resource NVARCHAR(25) not null,
       TrafficTypeID INT null,
       CategoryID INT null,
       UpdateUserID INT null,
       UpdateDate DATETIME null,
       CreateUserID INT null,
       CreateDate DATETIME null,
       DataStatus INT null,
       DeleteDate DATETIME null,
       primary key (Resource)
    )

    create table CRM_GEO_LOCATION_NUMBER_RANGES (
        LocationID BIGINT not null,
       Range1 NVARCHAR(25) null,
       Range2 NVARCHAR(25) null,
       NDC NVARCHAR(25) null,
       OperatorID INT null,
       NumberTypeID NVARCHAR(4) null,
       StartDate DATETIME null,
       primary key (LocationID)
    )

    create table CRM_MVNO_OPERATION_DETAILS_LOG (
        DetailOrderCode BIGINT not null,
       OperationResult NVARCHAR(100) null,
       Remarks NVARCHAR(2000) null,
       CreateDate DATETIME null,
       OrderCode BIGINT null,
       primary key (DetailOrderCode)
    )

    create table SYS25_LOG_OPERATION (
        ID BIGINT not null,
       MVNOID INT null,
       MVNONAME NVARCHAR(100) null,
       CUSTOMERNAME NVARCHAR(300) null,
       CUSTOMERID INT null,
       BUSINESS NVARCHAR(100) null,
       OPERATION NVARCHAR(100) null,
       OPERATIONLEVEL INT null,
       DETAIL NVARCHAR(MAX) null,
       IP NVARCHAR(100) null,
       CREATEDATE DATETIME null,
       CREATEUSERID INT null,
       CREATEUSERNAME NVARCHAR(100) null,
       SOURCENAME NVARCHAR(100) null,
       SOURCEID INT null,
       OPERATIONRESULT NVARCHAR(40) null,
       MSISDN NVARCHAR(25) null,
       ACTIONTYPE NVARCHAR(6) null,
       LOGTYPE INT null,
       OPERATIONCODE NVARCHAR(10) null,
       MESSAGES NVARCHAR(2000) null,
       MESSAGESID BIGINT null,
       OLDVALUE NVARCHAR(200) null,
       NEWVALUE NVARCHAR(200) null,
       primary key (ID)
    )

    create table CRM_MVNO_OPERATION_LOG (
        Code BIGINT not null,
       DEALERID INT null,
       OrderCode INT null,
       EXTERNALCODE NVARCHAR(50) null,
       OLDCODE BIGINT null,
       USERID INT null,
       VMO NVARCHAR(20) null,
       DESCRIPTION NVARCHAR(200) null,
       OPERATIONCODE NVARCHAR(3) null,
       REMARK NVARCHAR(2000) null,
       EXTERNAL_CHANNEL NVARCHAR(10) null,
       OPERATIONDATE DATETIME null,
       RESULT INT null,
       STATUS NVARCHAR(2) null,
       Messages NVARCHAR(255) null,
       SystemMessages NVARCHAR(MAX) null,
       INVOKEPARAMS VARBINARY(MAX) null,
       NAME1 NVARCHAR(50) null,
       NAME2 NVARCHAR(50) null,
       NAME3 NVARCHAR(50) null,
       NAME4 NVARCHAR(50) null,
       NAME5 NVARCHAR(50) null,
       NAME6 NVARCHAR(50) null,
       NAME7 NVARCHAR(50) null,
       NAME8 NVARCHAR(50) null,
       NAME9 NVARCHAR(50) null,
       NAME10 NVARCHAR(50) null,
       VALUE1 NVARCHAR(50) null,
       VALUE2 NVARCHAR(50) null,
       VALUE3 NVARCHAR(50) null,
       VALUE4 NVARCHAR(50) null,
       VALUE5 NVARCHAR(50) null,
       VALUE6 NVARCHAR(50) null,
       VALUE7 NVARCHAR(50) null,
       VALUE8 NVARCHAR(50) null,
       VALUE9 NVARCHAR(50) null,
       VALUE10 NVARCHAR(50) null,
       INVOKER INT null,
       TOPUPHISTORYID BIGINT null,
       LogID BIGINT null,
       TraceOrder NVARCHAR(50) null,
       ForeignCode NVARCHAR(50) null,
       ID BIGINT null,
       primary key (Code)
    )

    create table SYS25_ORGANIZATION (
        OrgID INT not null,
       ParentID INT null,
       OrgNode NVARCHAR(100) null,
       Contract NVARCHAR(40) null,
       TitleID INT null,
       GenderID INT null,
       Telephone NVARCHAR(50) null,
       TeleFax NVARCHAR(50) null,
       Email NVARCHAR(100) null,
       CreateDate DATETIME null,
       CreateUserID INT null,
       Status INT null,
       UpdateDate DATETIME null,
       UpdateUserID INT null,
       MVNOID INT null,
       primary key (OrgID)
    )

    create table CRM_DEALERS_PACKAGE_MAPPING (
        ID INT not null,
       DEALERID INT null,
       PACKAGEID INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       STATUS INT null,
       PRIORITY INT null,
       MVNOID INT null,
       CREATEDATE DATETIME null,
       primary key (ID)
    )

    create table RM_PACKAGE (
        PACKAGEID INT not null,
       DEALERID INT null,
       PACKAGENAME NVARCHAR(100) null,
       PAYMENTTYPEID INT null,
       SERVICETYPEID INT null,
       CREDITLIMIT DECIMAL(19,5) null,
       INVOICETEMPLATEID INT null,
       SPECIALCREDITLIMIT DECIMAL(19,5) null,
       PROMOTIONPACKAGEID INT null,
       PACKAGELEVEL INT null,
       primary key (PACKAGEID)
    )

    create table RM_PACKAGE_RELATIONSHIPS (
        ID INT not null,
       ORIGINPACKAGEID INT null,
       TARGETPACKAGEID INT null,
       primary key (ID),
      unique (ORIGINPACKAGEID, TARGETPACKAGEID)
    )

    create table Sys25_Performance_Trace_Config (
        ID BIGINT not null,
       AssembleName NVARCHAR(100) null,
       ClassName NVARCHAR(200) null,
       MethodName NVARCHAR(100) null,
       Description NVARCHAR(500) null,
       IsTrace INT null,
       CreateUserID INT null,
       CreateDate DATETIME null,
       primary key (ID)
    )

    create table SYS30_PERMISSION (
        PermissionID INT not null,
       FormName NVARCHAR(512) null,
       FullClassName NVARCHAR(512) null,
       ElementID NVARCHAR(200) null,
       RoleID INT null,
       ModuleID INT null,
       ElementName NVARCHAR(200) null,
       IsRead BIT null,
       IsWrite BIT null,
       IsHide BIT null,
       IsMandetory BIT null,
       Format NVARCHAR(100) null,
       Event NVARCHAR(100) null,
       DefaultValue NVARCHAR(100) null,
       XAXIS INT null,
       YAXIS INT null,
       primary key (PermissionID)
    )

    create table CRM_MOBILE_SIMCARD_PREACTIVATE_LOG (
        TRANSACTIONID NVARCHAR(36) not null,
       ICCID NVARCHAR(20) null,
       RESOURCE NVARCHAR(25) null,
       DEALERID INT null,
       STARTTIME DATETIME null,
       ENDTIME DATETIME null,
       MESSAGEID BIGINT null,
       DESCRIPTION NVARCHAR(510) null,
       USERID INT null,
       primary key (TRANSACTIONID)
    )

    create table VF_FILE_PRICEPLAN_PACKAGEID (
        VF_FILE_PRICEPLAN_PACKAGEID_ID INT not null,
       MVNO NVARCHAR(255) null,
       PRICEPLAN INT null,
       PACKAGEID INT null,
       usercategory NVARCHAR(255) null,
       primary key (VF_FILE_PRICEPLAN_PACKAGEID_ID)
    )

    create table CRM_CUSTOMERS_PRODUCTS (
        PRODUCTID INT not null,
       SERVICETYPEID INT null,
       CREDITLIMIT DECIMAL(19,5) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CREATEDATE DATETIME null,
       USERID INT null,
       SPECIALCREDITLIMIT DECIMAL(19,5) null,
       EXACTCREDITLIMIT DECIMAL(19,5) null,
       CUSTOMERID INT null,
       PackageID INT null,
       primary key (PRODUCTID)
    )

    create table CRM_CUSTOMERS_PROPERTY (
        PROPERTYID INT not null,
       CUSTOMERTYPEID INT null,
       LANGUAGEID INT null,
       PAYMENTMETHODID INT null,
       BILLINGMETHODID INT null,
       PARENT_BILLING BIT null,
       TRAFFICTYPEID INT null,
       TAXPLANID INT null,
       INVOICEDETAILS BIT null,
       EMAIL NVARCHAR(200) null,
       CountryCode INT null,
       CPSCode NVARCHAR(20) null,
       INVOICEDUEDATE INT null,
       BillingEntity INT null,
       WITHDRAWPERIOD INT null,
       USERNAME NVARCHAR(50) null,
       PASSWORD_DES NVARCHAR(50) null,
       PASSWORD_MD5 NVARCHAR(32) null,
       BIRTHDAY DATETIME null,
       CREATEDATE DATETIME null,
       USERID INT null,
       IDTYPE INT null,
       IDNUMBER NVARCHAR(100) null,
       CREDITSCORE INT null,
       ORIGINALDEPOSITAMOUNT DECIMAL(19,5) null,
       CURRENTDEPOSITAMOUNT DECIMAL(19,5) null,
       DEPOSITDATE DATETIME null,
       PENDINGSTATUS INT null,
       LOYALTYPOINT INT null,
       ACCEPTNEWS BIT null,
       LASTLOYALTYDATE DATETIME null,
       FF BIT null,
       BILLINGSCENARIOID INT null,
       CONTRACTPERIOD INT null,
       CREDITTRANSFERTYPE INT null,
       VATNO NVARCHAR(100) null,
       DEPOSITSTATUS INT null,
       CURRENTDEPOSITCREDIT DECIMAL(19,5) null,
       AUTOTOPUPSTATUS INT null,
       AUTOTOPUPAMOUNT DECIMAL(19,5) null,
       ACTIONCODE NVARCHAR(20) null,
       DATEUPDATED DATETIME null,
       IDEXPIREDATE DATETIME null,
       MAILTYPE NVARCHAR(1) null,
       SUBSCRIBERTYPE NVARCHAR(1) null,
       CONTRACTNO INT null,
       DMCEndUserId NVARCHAR(20) null,
       EXTERNAL_CUSTOMERID VARCHAR(64) null,
       NEEDLCSENDWELCOMESMS INT null,
       CPPCOUNTER_STARTDATE DATETIME null,
       CPPCOUNTER INT null,
       NEXT_PACKAGEID INT null,
       NEXT_PACKAGE_DATE DATETIME null,
       CUSTOMER_ROLE INT null,
       CASHDEPOSIT DECIMAL(19,5) null,
       ROAMINGDEPOSIT DECIMAL(19,5) null,
       REFERRER_CUSTOMERID INT null,
       LOW_BALANCE_ALERT_QUANTITY DECIMAL(19,5) null,
       DOCUMENT_VALIDATED_TIME DATETIME null,
       DOCUMENTVALIDATIONSTATUS INT null,
       DOCUMENTVALIDATIONREJECTREASON NVARCHAR(50) null,
       SYNCHRONIZE_WITH_ALELM NVARCHAR(1) null,
       CREDITADJUSTMENT_AMOUNT DECIMAL(19,5) null,
       PROFILEUPDATESOURCE INT null,
       CUSTOM_SWITCH_BITS BIGINT null,
       LOW_ROAMING_ALERT_QUANTITY DECIMAL(19,5) null,
       CUSTOMERID INT null,
       primary key (PROPERTYID)
    )

    create table RM_RATEPLAN_DETAILS (
        RATEPLANDETAILID INT not null,
       RATEPLANCATEGORYID INT null,
       RATEPLANID INT null,
       COUNTRYCODE NVARCHAR(25) null,
       PROVIDERID INT null,
       CALLTYPEID INT null,
       CALLDIRECTIONID INT null,
       TIMECATEGORYID INT null,
       UNITCATEGORYID INT null,
       CURRENCYID INT null,
       TARIFF1 DECIMAL(19,5) null,
       TARIFF2 DECIMAL(19,5) null,
       SETUP DECIMAL(19,5) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       PROMPT DECIMAL(19,5) null,
       ZONE1 INT null,
       ZONE2 INT null,
       PREFERRED BIT null,
       OPERATORCODE NVARCHAR(20) null,
       QUALITY INT null,
       RESTRICTION INT null,
       primary key (RATEPLANDETAILID)
    )

    create table RM_RATEPLAN (
        RATEPLANID INT not null,
       RATEPLANNAME NVARCHAR(100) null,
       DEALERID INT null,
       SERVICETYPEID INT null,
       SUBSERVICETYPEID INT null,
       RateType INT null,
       primary key (RATEPLANID)
    )

    create table RM_TRANSLATION (
        TRANSLATIONID INT not null,
       LANGUAGEID INT null,
       RATEPLANID INT null,
       COUNTRYCODE NVARCHAR(25) null,
       COUNTRYNAME NVARCHAR(510) null,
       primary key (TRANSLATIONID)
    )

    create table SYS25_REGEX (
        REGEX_ID INT IDENTITY NOT NULL,
       REGEX_TEXT NVARCHAR(510) null,
       REGEX_DESC NVARCHAR(510) null,
       INVALID_MESSAGE NVARCHAR(510) null,
       MESSAGEID BIGINT null,
       TYPEID INT null,
       primary key (REGEX_ID)
    )

    create table CRM_RELATE (
        ID INT not null,
       CARRIERID INT null,
       DEALERID INT null,
       CUSTOMERID INT null,
       TELEPHONENUMBER NVARCHAR(25) null,
       TARIFFCODE NVARCHAR(10) null,
       STARTPERIOD DATETIME null,
       ENDPERIOD DATETIME null,
       STATUSID INT null,
       primary key (ID)
    )

    create table CRM_LIFECYCLE_REPORTRESOURCES (
        REPORTID BIGINT not null,
       DEALERID INT null,
       RESOURCETYPE INT null,
       RESOURCENUMBER NVARCHAR(160) null,
       CURRENTSTATUSID INT null,
       NEXTSTATUSID INT null,
       CREATEDATE DATETIME null,
       ACTIONTYPE INT null,
       ISSENT BIT null,
       USERID INT null,
       primary key (REPORTID)
    )

    create table RM_BLACKWHITELIST (
        LISTID INT not null,
       LISTNAME NVARCHAR(40) null,
       LISTTYPEID INT null,
       REMARK NVARCHAR(200) null,
       primary key (LISTID)
    )

    create table RM_FF_FEE (
        FeeId INT not null,
       DealerId INT null,
       FeeTypeId INT null,
       ChargeTypeId INT null,
       TrafficTypeId INT null,
       PRICE DECIMAL(19,5) null,
       primary key (FeeId)
    )

    create table RM_FF_OPERATORLIST (
        DEALERID INT not null,
       OPERATORCODE NVARCHAR(10) null,
       primary key (DEALERID)
    )

    create table RM_FIX_NUMBERRANGE (
        DEALERID INT not null,
       RANGE1 NVARCHAR(25) null,
       RANGE2 NVARCHAR(25) null,
       primary key (DEALERID)
    )

    create table RM_PROMOTIONGROUPS (
        PROMOTIONGROUPID INT not null,
       MVNOID INT null,
       NAME NVARCHAR(255) null,
       DESCRIPTION NVARCHAR(255) null,
       PRICE DECIMAL(19,5) null,
       VALIDITY INT null,
       PRIORITY INT null,
       GROUPTYPE INT null,
       GROUPCATEGORY INT null,
       STARTPERIOD INT null,
       ENDPERIOD INT null,
       GRACEPERIOD INT null,
       primary key (PROMOTIONGROUPID)
    )

    create table RM_PROMOTIONGROUP_MEMBERS (
        PROMOTIONGROUPID INT not null,
       PROMOTIONPLANID INT not null,
       LIMIT DECIMAL(19,5) null,
       MAXMSISNDNUMBERSIZE INT null,
       primary key (PROMOTIONGROUPID, PROMOTIONPLANID)
    )

    create table RM_PROMOTIONPLAN_DETAIL (
        PROMOTIONPLANDETAILID INT not null,
       PROMOTIONTYPEID INT null,
       SERVICETYPEID INT null,
       SUBSERVICETYPEID INT null,
       DATECATEGORYID INT null,
       CALLDIRECTIONID INT null,
       LIMITPERCALL DECIMAL(19,5) null,
       LIMIT DECIMAL(19,5) null,
       LIMITUNIT INT null,
       CURRENCYID INT null,
       PROMOTIONMETHODID INT null,
       SETUP DECIMAL(19,5) null,
       PROMPT DECIMAL(19,5) null,
       TARIFF1 DECIMAL(19,5) null,
       TARIFF2 DECIMAL(19,5) null,
       DISCOUNTMETHODID INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       WHITELIST NVARCHAR(500) null,
       APPLYONROAMING INT null,
       USAGEFEE DECIMAL(19,5) null,
       RatePlanId INT null,
       BasePromotionPlanDetailId INT null,
       PROMOTIONPLANDETAILNAME NVARCHAR(80) null,
       ApplyOnSuperOnNet BIT null,
       OVERLIMITRATEPLANID INT null,
       BLACKLISTID INT null,
       MAXIMUM_ALLOWED_BALANCE DECIMAL(19,5) null,
       WALLETTYPEID INT null,
       APIVISIBLE INT null,
       PERIOD_UNITID INT null,
       PERIOD_UNIT_COUNT INT null,
       START_PERIODNUMBER INT null,
       END_PERIODNUMBER INT null,
       PERIODICITY INT null,
       CYCLE_REPEAT_COUNT INT null,
       PRERENEWALACTIONS_MINUTES_OFFSET INT null,
       PROMOTIONPLANID INT null,
       primary key (PROMOTIONPLANDETAILID)
    )

    create table RM_PROMOTIONPLAN (
        PROMOTIONPLANID INT not null,
       PROMOTIONPLANNAME NVARCHAR(200) null,
       DEALERID INT null,
       EXCLUSIVE BIT null,
       PERIODIC INT null,
       PRIORITY INT null,
       RESETLIMIT BIT null,
       MONTHFEE DECIMAL(19,5) null,
       SUBSCRIPTIONFEE DECIMAL(19,5) null,
       SUBSCRIPTIONPERIOD INT null,
       SUBSCRIPTIONPERIODUNIT INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       ActiveWithoutCredit INT null,
       PROMOTIONCATEGORYID INT null,
       SELFCAREVISIBLE INT null,
       CUSTOMERCAREVISIBLE INT null,
       APIVISIBLE INT null,
       PRORATE INT null,
       RESETPERIOD INT null,
       RESETPERIODUNIT INT null,
       DISCOUNTMONTHFEEFORRENEW DECIMAL(19,5) null,
       TIMEPOINTFORCHARGEFEE INT null,
       SMSNOTIFICATION INT null,
       NOTIFICATIONSMSTEMPLATEID INT null,
       RENEWAUTOMATICALLY INT null,
       DAILYRESETTIME INT null,
       RESTRICTUNIT INT null,
       RESTRICTDURATION INT null,
       REMOVEIMMEDIATELYFLAG INT null,
       ACCUMULATIVE BIT null,
       PROMOTIONGROUPID INT null,
       MSISDNGROUPTYPEID INT null,
       primary key (PROMOTIONPLANID)
    )

    create table RM_PROMOTIONPLAN_RULE (
        PROMOTIONPLANRULEID INT not null,
       ACTIVATIONTYPE INT null,
       SERVICETYPE INT null,
       SUBSERVICETYPE INT null,
       PACKAGEID INT null,
       BUSINESSTYPE INT null,
       CALCULATETYPE INT null,
       CALCULATEUNIT INT null,
       SCALAR DECIMAL(19,5) null,
       RULEID INT null,
       RETURNMETHOD INT null,
       VALIDTIME INT null,
       REBATETYPE INT null,
       REBATEVALUE DECIMAL(19,5) null,
       REBATEVALUEUNIT INT null,
       CURRENCYID INT null,
       RULEDESC NVARCHAR(100) null,
       SCANSTATUS INT null,
       LASTSCANTIME DATETIME null,
       PROMOTIONPLANID INT null,
       primary key (PROMOTIONPLANRULEID)
    )

    create table RM_PROMOTIONPLAN_TOPUP_BASED_PARAMETERS (
        PROMOTIONPLANID INT not null,
       SUBSERVICETYPEID INT not null,
       TOPUP_THRESHOLD_AMOUNT DECIMAL(19,5) not null,
       DAYS_TO_ACCCUMULATE_TOPUPS INT null,
       VALIDITY_UNIT INT null,
       VALIDITY_QTY INT null,
       LIMIT_UNIT INT null,
       LIMIT_QTY DECIMAL(19,5) null,
       primary key (PROMOTIONPLANID, SUBSERVICETYPEID, TOPUP_THRESHOLD_AMOUNT)
    )

    create table RM_SPECIFICNUMBER_GROUP (
        GROUPID INT not null,
       GROUPNAME NVARCHAR(200) null,
       SPECIFICNUMBERCATEGORYID INT null,
       TRAFFICTYPEID INT null,
       SERVICETYPEID INT null,
       SUBSERVICETYPEID INT null,
       DEALERID INT null,
       SETUP DECIMAL(19,5) null,
       TARIFF1 DECIMAL(19,5) null,
       TARIFF2 DECIMAL(19,5) null,
       DISCOUNTMETHODID INT null,
       MAXNUMBERCOUNT INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       PROMOTIONPLANID INT null,
       primary key (GROUPID)
    )

    create table CRM_ROAMING_BLACKLIST (
        BLACKLISTID INT not null,
       DEALERID INT null,
       MSISDN NVARCHAR(15) null,
       CREATEDATE DATETIME null,
       DESCRIPTION NVARCHAR(256) null,
       CUSTOMERID INT null,
       primary key (BLACKLISTID)
    )

    create table RM_ROAMING_OPERATORS_E164 (
        ID INT not null,
       CCNDC NVARCHAR(20) null,
       TSC NVARCHAR(20) null,
       COUNTRYNAME NVARCHAR(400) null,
       OPERATORNAME NVARCHAR(400) null,
       CC NVARCHAR(10) null,
       NDC NVARCHAR(10) null,
       COUNTRYISO2CODE NVARCHAR(20) null,
       COUNTRYISO3CODE NVARCHAR(20) null,
       primary key (ID)
    )

    create table CRM_ROAMING_SETTING (
        SETTINGID INT not null,
       ENABLE_BAOC_PREPAID BIT null,
       ENABLE_BAOC_POSTPAID BIT null,
       RECEIVE_SMS_POSTPAID BIT null,
       RECEIVE_SMS_PREPAID BIT null,
       RECEIVE_SMS_NONECAMEL BIT null,
       RECEIVE_SMS_CAMEL BIT null,
       ACTIVATED BIT null,
       FTPSERVER NVARCHAR(256) null,
       FTPPORT INT null,
       FOLDER NVARCHAR(256) null,
       USERNAME NVARCHAR(256) null,
       PASSWORD NVARCHAR(256) null,
       IMPORTWAY INT null,
       CREATEDATE DATETIME null,
       DealerID INT null,
       primary key (SETTINGID)
    )

    create table CRM_ROAMING_SMS_CONFIG (
        ID INT not null,
       DEALERID INT null,
       BundleID INT null,
       ZoneID INT null,
       StartDate DATETIME null,
       EndDate DATETIME null,
       SMSTemplateId INT null,
       SMSType INT null,
       Status INT null,
       NetType INT null,
       PaymentType INT null,
       Description NVARCHAR(512) null,
       primary key (ID)
    )

    create table CRM_Roaming_SMS_Histrory (
        ID BIGINT not null,
       MSISDN NVARCHAR(16) null,
       OMV NVARCHAR(16) null,
       OperatorRoaming NVARCHAR(10) null,
       ZONECODE NVARCHAR(16) null,
       SMS NVARCHAR(800) null,
       NetType NVARCHAR(50) null,
       PaymentType NVARCHAR(50) null,
       CountryName NVARCHAR(400) null,
       OperatorName NVARCHAR(400) null,
       BundleId INT null,
       CREATEDATE DATETIME null,
       primary key (ID)
    )

    create table RM_ROAMING_ZONE_DETAILS (
        ZONEDETAILID INT not null,
       CCNDC NVARCHAR(20) null,
       TSC NVARCHAR(20) null,
       PREFERRED BIT null,
       COUNTRYNAME NVARCHAR(400) null,
       OPERATORNAME NVARCHAR(400) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       ZoneID INT null,
       primary key (ZONEDETAILID)
    )

    create table RM_ROAMING_ZONE (
        ZONEID INT not null,
       ZONENAME NVARCHAR(100) null,
       DEALERID INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       SUBSERVICETYPEID INT null,
       TRAFFICTYPEID INT null,
       primary key (ZONEID)
    )

    create table SYS25_ROLE (
        RoleID INT not null,
       ModuleID INT null,
       DealerID INT null,
       RoleName NVARCHAR(100) null,
       ModuleName NVARCHAR(100) null,
       DealerNode NVARCHAR(100) null,
       CreateDate DATETIME null,
       CreateUserID INT null,
       Status INT null,
       UpdateDate DATETIME null,
       UpdateUserID INT null,
       primary key (RoleID)
    )

    create table SYS25_ROLE_PERMISSION (
        ID INT IDENTITY NOT NULL,
       RoleID INT null,
       PermissionID INT null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_SERVICES (
        SERVICEID INT not null,
       CREDITLIMIT DECIMAL(19,5) null,
       UNBILLEDBALANCE DECIMAL(19,5) null,
       BILLEDBALANCE DECIMAL(19,5) null,
       INVOICETEMPLATEID INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CREATEDATE DATETIME null,
       USERID INT null,
       DEPOSITAMOUNT FLOAT(53) null,
       UPDATESTAMP VARBINARY(MAX) null,
       CREDITLIMITBASEBUNDLEID INT null,
       STATUS INT null,
       ProductID INT null,
       CUSTOMERID INT null,
       BUNDLEID INT null,
       primary key (SERVICEID)
    )

    create table CRM_LIFECYCLE_SETTINGS_DETAILS (
        DETAILID INT not null,
       SUBITEMID INT null,
       DESCRIPTION NVARCHAR(600) null,
       INTERVAL INT null,
       UNIT NVARCHAR(60) null,
       ENABLED BIT null,
       SENDREPORT BIT null,
       SENDSMS BIT null,
       CREATEDATE DATETIME null,
       SettingId INT null,
       primary key (DETAILID)
    )

    create table CRM_LIFECYCLE_SETTINGS_EXTEND_DETAILS (
        SETTINGID INT not null,
       DEALERID INT null,
       CATEGORYID INT null,
       STATUSID INT null,
       ITEMID INT null,
       ITEMNAME NVARCHAR(400) null,
       ITEMVALUE NVARCHAR(800) null,
       ITEMDESCRIPTION NVARCHAR(1600) null,
       itemStart DECIMAL(19,5) null,
       itemEnd DECIMAL(19,5) null,
       primary key (SETTINGID)
    )

    create table CRM_LIFECYCLE_SETTINGS (
        SETTINGID INT not null,
       DESCRIPTION NVARCHAR(400) null,
       DEALERID INT null,
       ENABLED BIT null,
       SCHEDULINTERVALDAYS INT null,
       SCHEDULETYPE INT null,
       SCHEDULORIGINALDATE DATETIME null,
       SCHEDULTIME NVARCHAR(12) null,
       SCHEDULUNIT NVARCHAR(20) null,
       CREATEDATE DATETIME null,
       EventId INT null,
       primary key (SETTINGID)
    )

    create table CRM_CUSTOMERS_IMSI_MAP (
        TEMPIMSI NVARCHAR(32) not null,
       TEMPMSISDN NVARCHAR(255) null,
       PERMANENTIMSI NVARCHAR(32) null,
       primary key (TEMPIMSI)
    )

    create table CRM_MOBILE_SIMCARDS_ORDER (
        ORDERID INT not null,
       BATCH NVARCHAR(5) null,
       DEALERID INT null,
       ORDERDATE DATETIME null,
       QUANTITY INT null,
       COMPANY NVARCHAR(200) null,
       ADDRESS NVARCHAR(200) null,
       CITY NVARCHAR(200) null,
       COUNTRY NVARCHAR(200) null,
       STATUS INT null,
       ACTIVATETYPE INT null,
       primary key (ORDERID)
    )

    create table CRM_SMS_HISTORY (
        LOGID BIGINT not null,
       DEALERID INT null,
       SENDER NVARCHAR(255) null,
       MSISDN NVARCHAR(255) null,
       SMSMESSAGE NVARCHAR(500) null,
       CATEGORYID INT null,
       PRIORITY INT null,
       VALIDITY INT null,
       TRYSENTTIMES INT null,
       MAXSENTTIMES INT null,
       NAME1 NVARCHAR(255) null,
       NAME2 NVARCHAR(255) null,
       NAME3 NVARCHAR(255) null,
       NAME4 NVARCHAR(255) null,
       NAME5 NVARCHAR(255) null,
       NAME6 NVARCHAR(255) null,
       VALUE1 NVARCHAR(255) null,
       VALUE2 NVARCHAR(255) null,
       VALUE3 NVARCHAR(255) null,
       VALUE4 NVARCHAR(255) null,
       VALUE5 NVARCHAR(255) null,
       VALUE6 NVARCHAR(255) null,
       ISSENT BIT null,
       VIRTUALDELETED BIT null,
       CREATEDATE DATETIME null,
       SENTDATE DATETIME null,
       USERID INT null,
       IsSmsInQueue INT null,
       DELIVERYSTATUS INT null,
       ScheduleDeliveryTime DATETIME null,
       primary key (LOGID)
    )

    create table CRM_SMS_QUEUE (
        DEALERID INT not null,
       UPDATESTAMP VARBINARY(MAX) null,
       FlagImmediately INT null,
       FlagOther INT null,
       NEXTSCANTIME DATETIME null,
       primary key (DEALERID)
    )

    create table CRM_MVNO_TEXT_TEMPLATE (
        ID INT not null,
       MVNOID INT null,
       LANGUAGEID INT null,
       CODE INT null,
       DESCRIPTION NVARCHAR(100) null,
       TITLE NVARCHAR(100) null,
       TITLEARGS NVARCHAR(50) null,
       BODY NVARCHAR(1000) null,
       BODYARGS NVARCHAR(50) null,
       SMSCODING INT null,
       ENABLED BIT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       primary key (ID)
    )

    create table CRM_LIFECYCLE_SMS_TEMPLETS (
        TEMPLETID INT not null,
       LANGUAGEID INT null,
       CONTENT NVARCHAR(400) null,
       SettingId INT null,
       primary key (TEMPLETID)
    )

    create table CRM_CUSTOMERS_SPECIALTELNUMBER (
        SECURITYNUMBERID BIGINT not null,
       CUSTOMERID INT null,
       PHONENUMBER NVARCHAR(50) null,
       TYPE INT null,
       CREATEDATE DATETIME null,
       DECRIPTION NVARCHAR(200) null,
       primary key (SECURITYNUMBERID)
    )

    create table CRM_LIFECYCLE_RESOURCE_STATUSCHANGED_LOG (
        LOGID BIGINT not null,
       CUSTOMERID INT null,
       DEALERID INT null,
       DEALERNAME NVARCHAR(200) null,
       CUSTOMERNAME NVARCHAR(300) null,
       RESOURCETYPEID INT null,
       RESOURCETYPENAME NVARCHAR(200) null,
       RESOURCENUMBER NVARCHAR(300) null,
       CURRENTSTATUSID INT null,
       NEXTSTATUSID INT null,
       EVENTID INT null,
       EVENTNAME NVARCHAR(200) null,
       USERID INT null,
       USERNAME NVARCHAR(200) null,
       CREATEDATE DATETIME null,
       primary key (LOGID)
    )

    create table SYS25_STYLE (
        ID INT not null,
       StyleID INT null,
       StyleName NVARCHAR(100) null,
       ORGID INT null,
       Image VARBINARY(MAX) null,
       ImageName NVARCHAR(100) null,
       StyleType INT null,
       IndexID INT null,
       UpdateDate DATETIME null,
       primary key (ID)
    )

    create table SYS25_CONFIG_DATA (
        ITEM NVARCHAR(100) not null,
       NAME NVARCHAR(100) null,
       VALUE NVARCHAR(MAX) null,
       DESCRIPTION NVARCHAR(200) null,
       VERSION NVARCHAR(100) null,
       primary key (ITEM)
    )

    create table SYS_ID (
        IDNAME NVARCHAR(50) not null,
       IDVALUE INT null,
       primary key (IDNAME)
    )

    create table SYS25_THEME (
        ID INT not null,
       SkinID NVARCHAR(100) null,
       ThemeID INT null,
       ControlClassName NVARCHAR(512) null,
       ItemName NVARCHAR(100) null,
       ItemValue NVARCHAR(1024) null,
       Description NVARCHAR(512) null,
       MVNOID INT null,
       primary key (ID)
    )

    create table RM_TIMECATEGORY (
        TIMECATEGORYID INT not null,
       TIMECATEGORYSEQ INT not null,
       TIMECATEGORYNAME NVARCHAR(100) null,
       STARTTIME INT null,
       ENDTIME INT null,
       DAYOFWEEK NVARCHAR(15) null,
       REMARK NVARCHAR(100) null,
       primary key (TIMECATEGORYID, TIMECATEGORYSEQ)
    )

    create table CRM_CUSTOMERS_TRANSFER_HISTORY (
        TRANSFERID INT not null,
       DEALERID INT null,
       MVNOID INT null,
       FROMMSISDN NVARCHAR(25) null,
       TOMSISDN NVARCHAR(25) null,
       FROMCUSTOMERID INT null,
       TOCUSTOMERID INT null,
       PINCODE NVARCHAR(10) null,
       TRANSFERAMOUNT DECIMAL(19,5) null,
       CONFIRMFLAG INT null,
       EXPIREDATE DATETIME null,
       CREATEDATE DATETIME null,
       APPLYLOGID BIGINT null,
       CONFIRMLOGID BIGINT null,
       DESCRIPTION NVARCHAR(100) null,
       USERID INT null,
       primary key (TRANSFERID)
    )

    create table CRM_LIFECYCLE_TRANSITIONS (
        TRANSITIONID INT not null,
       EVENTID INT null,
       CURRENTSTATUSID INT null,
       NEXTSTATUSID INT null,
       ACTIONID INT null,
       NEXTNPMSTATUS INT null,
       NEXTICCSTATUS INT null,
       primary key (TRANSITIONID)
    )

    create table CRM_TROUBLE_TICKET (
        ID INT not null,
       TICKETNUMBER NVARCHAR(20) null,
       TTCODE NVARCHAR(50) null,
       CLASSID INT null,
       PRIORITY INT null,
       FUID INT null,
       DEPTID INT null,
       OPERATORID INT null,
       HANDLEBY INT null,
       DESCRIPTION NVARCHAR(3000) null,
       ETACK DATETIME null,
       REPORTTIME DATETIME null,
       UPDATETIME DATETIME null,
       STATUS INT null,
       CUSTOMERID INT null,
       MSISDN NVARCHAR(20) null,
       DIALEDNO NVARCHAR(20) null,
       CALLTIME DATETIME null,
       NETWORK NVARCHAR(50) null,
       CALLPLACE NVARCHAR(50) null,
       CLIENTID NVARCHAR(50) null,
       SECONDNUMBER NVARCHAR(50) null,
       FLEXNUMBER NVARCHAR(50) null,
       INTERNALNO NVARCHAR(50) null,
       CUSTTYPE INT null,
       EXTERNALCODE NVARCHAR(50) null,
       primary key (ID)
    )

    create table CRM_TT_QUESTION_LIB (
        ID INT not null,
       TROUBLETYPE NVARCHAR(10) null,
       TTSUBTYPE NVARCHAR(20) null,
       QUESTIONCODE INT null,
       TROUBLEDESC NVARCHAR(150) null,
       TTSUBDESC NVARCHAR(300) null,
       QUESTIONDESC NVARCHAR(250) null,
       MVNOID INT null,
       primary key (ID)
    )

    create table CRM_TT_DEPT (
        DEPTID INT not null,
       FUID INT null,
       DEPTNAME NVARCHAR(50) null,
       DEPTHEADER INT null,
       DEPTPARENT INT null,
       DEPTROLEID INT null,
       DEPTID25 INT null,
       primary key (DEPTID)
    )

    create table CRM_TT_HISTORY (
        ID INT not null,
       TICKETNUMBER NVARCHAR(20) null,
       USERID INT null,
       COMMENT NVARCHAR(3000) null,
       UPDATETIME DATETIME null,
       COMMENTTYPE INT null,
       primary key (ID)
    )

    create table RM_UNITCATEGORY (
        UNITCATEGORYID INT not null,
       UNITCATEGORYNAME NVARCHAR(100) null,
       ROUNDUP BIT null,
       UNIT1 INT null,
       UNIT2 INT null,
       PROMPT INT null,
       primary key (UNITCATEGORYID)
    )

    create table SYS25_USER_DEALER (
        ID INT not null,
       UserID INT null,
       DealerID INT null,
       primary key (ID)
    )

    create table SYS25_USER_ORG (
        ID INT not null,
       UserID INT null,
       OrganizationID INT null,
       primary key (ID)
    )

    create table SYS25_USER_ROLE (
        ID INT not null,
       UserID INT null,
       RoleID INT null,
       primary key (ID)
    )

    create table CRM_DEALERS_VOUCHER_BATCH_INFO (
        BatchId NVARCHAR(80) not null,
       DealerId INT null,
       SnStart NVARCHAR(80) null,
       SnEnd NVARCHAR(80) null,
       Credit DECIMAL(19,5) null,
       CreateDate DATETIME null,
       VKey NVARCHAR(16) null,
       Supplier NVARCHAR(50) null,
       PONUMBER INT null,
       PACKAGETYPE INT null,
       PACKAGETYPEINFO NVARCHAR(40) null,
       ORGIDCURRENTMVO INT null,
       ORGANIZATIONIDCURRENTMVNO INT null,
       PGPFILENAME NVARCHAR(80) null,
       primary key (BatchId)
    )

    create table CRM_DEALERS_VOUCHER_BAK (
        BakId BIGINT not null,
       VoucherCode NVARCHAR(80) null,
       SN NVARCHAR(80) null,
       VCENCRYPT NVARCHAR(50) null,
       DEALERID INT null,
       CMSDEALERID INT null,
       INITIALCREDIT DECIMAL(19,5) null,
       CURRENTCREDIT DECIMAL(19,5) null,
       CURRENCYID INT null,
       CREATEDATE DATETIME null,
       STATUS INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CHANGESTATUSDATE DATETIME null,
       ACTIVEDEADLINEDATE DATETIME null,
       VATRATE DECIMAL(19,5) null,
       CREDITWITHVAT DECIMAL(19,5) null,
       primary key (BakId)
    )

    create table CRM_DEALERS_VOUCHER (
        VoucherCode NVARCHAR(40) not null,
       SN NVARCHAR(80) null,
       VCENCRYPT NVARCHAR(50) null,
       DEALERID INT null,
       CMSDEALERID INT null,
       INITIALCREDIT DECIMAL(19,5) null,
       CURRENTCREDIT DECIMAL(19,5) null,
       CURRENCYID INT null,
       CREATEDATE DATETIME null,
       STATUS INT null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CHANGESTATUSDATE DATETIME null,
       ACTIVEDEADLINEDATE DATETIME null,
       VATRATE DECIMAL(19,5) null,
       TAXCODE INT null,
       CREDITWITHVAT DECIMAL(19,5) null,
       VOUCHERTYPE INT null,
       primary key (VoucherCode)
    )

    create table CRM_MVNO_VPN_BUSINESS_GROUP (
        GROUPID INT not null,
       VPNID INT null,
       GROUPCODE NVARCHAR(2) null,
       GROUPNAME NVARCHAR(40) null,
       NUMBERCATEGORYID INT null,
       STATUSID INT null,
       CREATEDATE DATETIME null,
       REMARK NVARCHAR(200) null,
       primary key (GROUPID)
    )

    create table CRM_MVNO_VPN (
        VPNID INT not null,
       DEALERID INT null,
       VPNNAME NVARCHAR(40) null,
       GROUPCODELENGTH INT null,
       SHORTCODELENGTH INT null,
       CREATEDATE DATETIME null,
       REMARK NVARCHAR(200) null,
       primary key (VPNID)
    )

    create table CRM_MVNO_VPN_SHORTCODE (
        SHORTCODEID INT not null,
       GROUPID INT null,
       SHORTCODE NVARCHAR(10) null,
       STATUSID INT null,
       CREATEDATE DATETIME null,
       REMARK NVARCHAR(200) null,
       primary key (SHORTCODEID)
    )

    create table CRM_MVNO_VPN_SUBSCRIBER (
        SUBSCRIBERID INT not null,
       GROUPID INT null,
       RESOURCEID INT null,
       RESOURCETYPEID INT null,
       SHORTCODEID INT null,
       STATUSID INT null,
       ENDDATE DATETIME null,
       STARTDATE DATETIME null,
       CREATEDATE DATETIME null,
       REMARK NVARCHAR(200) null,
       primary key (SUBSCRIBERID)
    )

    create table CRM_DEALERS_OBOPRS (
        OBOPRSID INT not null,
       PREFIX NVARCHAR(25) null,
       PRSTYPEID INT null,
       CREATEDATE DATETIME null,
       USERID INT null,
       DealerID INT null,
       primary key (OBOPRSID)
    )

    create table CRM_MOBILE_MULTIPLEIMSI (
        IMSIID INT not null,
       ALTIMSI NVARCHAR(15) null,
       ALTMSISDN NVARCHAR(15) null,
       PUBLISHED BIT null,
       DISPLAYED BIT null,
       Status INT null,
       RESOURCEID INT null,
       primary key (IMSIID)
    )

    create table CRM_MOBILE_NETWORK (
        NETWORKID INT not null,
       NetworkType INT null,
       PDPCONTEXTID INT null,
       PDPContextAddress NVARCHAR(30) null,
       QosGuaranteedBitRateUp INT null,
       QosGuaranteedBitRateDown INT null,
       APN NVARCHAR(100) null,
       MMS BIT null,
       WAP BIT null,
       QosMaxBitRateUp INT null,
       QosMaxBitRateDown INT null,
       OBOPRE BIT null,
       OBOPRI BIT null,
       FreeWebsiteThreshold INT null,
       AdultContentBarringStatus INT null,
       FreeWebsiteStatus INT null,
       RESOURCEID INT null,
       primary key (NETWORKID)
    )

    create table CRM_DEFAULT_PROVISION_NETWORK (
        NETWORKID INT not null,
       NAM INT null,
       NetworkType INT null,
       APN NVARCHAR(100) null,
       MMS BIT null,
       WAP BIT null,
       OBOPRE BIT null,
       OBOPRI BIT null,
       PDPCONTEXTID INT null,
       PDPTYPE INT null,
       PDPADDRESS NVARCHAR(127) null,
       VPLMNADDRESSALLOWED BIT null,
       QOSALLOCATIONRETENTIONPRIORITY INT null,
       QOSTRAFFICCLASS INT null,
       QOSDELIVERYERRONEOUSSDU INT null,
       QOSMAXSDUSIZE INT null,
       QOSDELIVERYORDER INT null,
       QOSMAXBITRATEDOWN INT null,
       QOSMAXBITRATEUP INT null,
       QOSRESIDUALBER INT null,
       QOSSDUERRORRATIO INT null,
       QOSTRANSFERDELAY INT null,
       QOSTRAFFICHANDLINGPRIORITY INT null,
       QOSGUARANTEEDBITRATEUP INT null,
       QOSGUARANTEEDBITRATEDOWN INT null,
       QOSSIGNALLINGINDICATION INT null,
       PROVISIONID INT null,
       primary key (NETWORKID)
    )

    create table CRM_DEFAULT_PROVISION_ROAMING_SETTING (
        SETTINGID INT not null,
       SMSTEXT NVARCHAR(400) null,
       SMSTEXT_CAMEL NVARCHAR(400) null,
       ENABLEPREPAID BIT null,
       ENABLEPOSTPAID BIT null,
       Activated BIT null,
       ENABLESMS_CAMEL BIT null,
       EnableSms BIT null,
       DealerId INT null,
       primary key (SETTINGID)
    )

    create table CRM_CUSTOMERS_REMARKS (
        REMARKID INT not null,
       REMARK NVARCHAR(MAX) null,
       SUBJECT NVARCHAR(MAX) null,
       CREATEDATE DATETIME null,
       USERID INT null,
       ATTACHFILENAME NVARCHAR(255) null,
       StatusID INT null,
       AttachFile VARBINARY(MAX) null,
       CUSTOMERID INT null,
       primary key (REMARKID)
    )

    create table HLR_RequestErrors (
        SEQID BIGINT not null,
       OBJECTNAME NVARCHAR(100) null,
       QueueId BIGINT null,
       ERRORCODE NVARCHAR(10) null,
       ResponseMessage NVARCHAR(MAX) null,
       RequestMessage NVARCHAR(MAX) null,
       REPEAT INT null,
       CREATETIME DATETIME null,
       primary key (SEQID)
    )

    create table CRM_BUSINESSRULES_PROMOTIONGROUPS (
        ID INT not null,
       Rank SMALLINT null,
       PromotionGroupID  INT null,
       RuleID INT null,
       PromotionGroupID INT null,
       primary key (ID)
    )

    create table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY (
        NHIBERNATE_KEY BIGINT not null,
       TRANSACTIONTIME DATETIME null,
       AMOUNT_AFTER DECIMAL(22, 8) null,
       AMOUNT_BEFORE DECIMAL(22, 8) null,
       EXTERNAL_REASONCODE VARCHAR(255) null,
       COMMENTS NVARCHAR(255) null,
       LOGID BIGINT null,
       ID_UNIT INT null,
       SERVICEID INT null,
       PROMOTIONID BIGINT null,
       OPERATION_EXECUTIONID BIGINT null,
       CUSTOMERID INT null,
       PROMOTIONPLANID INT null,
       RESOURCEID INT null,
       PACKAGEID INT null,
       primary key (NHIBERNATE_KEY)
    )

    create table dbo.ADDRESSES (
        ADDRESSID INT not null,
       ADDRESS NVARCHAR(255) null,
       AREA NVARCHAR(255) null,
       BLOCK NVARCHAR(255) null,
       BUILDINGDOOR NVARCHAR(255) null,
       CITY NVARCHAR(255) null,
       COMMENTS NVARCHAR(255) null,
       COUNTRYID INT null,
       CREATEDATE DATETIME null,
       CREATEUSER INT null,
       DOOR NVARCHAR(255) null,
       HOUSEEXTENSION NVARCHAR(255) null,
       HOUSENO NVARCHAR(255) null,
       NEIGHBORHOOD NVARCHAR(255) null,
       POBOX INT null,
       PORTAL NVARCHAR(255) null,
       STAIR NVARCHAR(255) null,
       STATE NVARCHAR(255) null,
       STATUS INT null,
       SUBURB NVARCHAR(255) null,
       ZIPCODE NVARCHAR(255) null,
       primary key (ADDRESSID)
    )

    create table dbo.TEXT_LOOKUP_TRANSLATIONS (
        LOOKUPID INT not null,
       LANGUAGEID INT not null,
       LOOKUPSTRING NVARCHAR(255) null,
       primary key (LOOKUPID, LANGUAGEID)
    )

    create table dbo.TEXT_LOOKUPS (
        LOOKUPID INT not null,
       LOOKUPSTRING NVARCHAR(255) null,
       primary key (LOOKUPID)
    )

    create table RM_OPERATORS (
        OPERATORCODE NVARCHAR(10) not null,
       OPERANAME NVARCHAR(200) null,
       COUNTRYNAME NVARCHAR(200) null,
       NetworkOperator NVARCHAR(10) null,
       IndividualQuota INT null,
       MultQuota INT null,
       ChangeWindowQuota INT null,
       Status NVARCHAR(10) null,
       StatusDate DATETIME null,
       CNOperatorCode NVARCHAR(255) null,
       primary key (OPERATORCODE)
    )

    create table CRM_OPERATION_EXECUTIONS (
        OPERATION_EXECUTIONID BIGINT not null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       EXTERNAL_CHANNEL VARCHAR(255) null,
       AMOUNT DECIMAL(22, 8) null,
       RESULTTYPE INT null,
       ERRORCODE INT null,
       ERRORMESSAGE NVARCHAR(1024) null,
       OPERATIONID INT null,
       OPERATION_CONFIGURATIONID INT null,
       PARENT_OPERATION_EXECUTIONID BIGINT null,
       ROOT_OPERATION_EXECUTIONID BIGINT null,
       USERID INT null,
       FISCALUNITID INT null,
       CUSTOMERID INT null,
       DESTINATION_CUSTOMERID INT null,
       RESOURCEID INT null,
       DESTINATION_RESOURCEID INT null,
       MSISDN VARCHAR(25) null,
       ACCOUNTID BIGINT null,
       PRODUCTGK INT null,
       CUSTOMERPRODUCTID BIGINT null,
       ICCID VARCHAR(20) null,
       ORDERID BIGINT null,
       primary key (OPERATION_EXECUTIONID)
    )

    create table CRM_OPERATIONS (
        OPERATIONID INT not null,
       OPERATION_DISCRIMINATOR NVARCHAR(6) not null,
       OPERATIONCODE VARCHAR(6) null,
       DESCRIPTIONID INT null,
       primary key (OPERATIONID)
    )

    create table CRM_OPERATION_CONFIGURATIONS (
        OPERATION_CONFIGURATIONID INT not null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       JSON_CONFIGURATION VARCHAR(MAX) null,
       DEALERID INT null,
       OPERATIONID INT null,
       primary key (OPERATION_CONFIGURATIONID)
    )

    create table CRM_ORDERS (
        ORDERID BIGINT not null,
       ORDER_DISCRIMINATOR NVARCHAR(6) not null,
       COMPLETIONDATE DATETIME null,
       CREATEDATE DATETIME null,
       EXTERNALID VARCHAR(255) null,
       LAST_UPDATEDATE DATETIME null,
       STATUS VARCHAR(4) null,
       THIRD_PARTYID VARCHAR(255) null,
       EXTERNAL_THIRD_PARTYID VARCHAR(255) null,
       FISCALUNITID INT null,
       PARENT_ORDERID BIGINT null,
       STARTING_OPERATIONEXECUTIONID BIGINT null,
       primary key (ORDERID)
    )

    create table CRM_ORDER_STATUS_HISTORY (
        ORDER_STATUS_HISTORYID BIGINT not null,
       STATUSCHANGEDATE DATETIME2 null,
       NEW_STATUS VARCHAR(4) null,
       OLD_STATUS VARCHAR(4) null,
       TRANSITION_CODE INT null,
       ORDERID BIGINT null,
       OPERATION_EXECUTIONID BIGINT null,
       primary key (ORDER_STATUS_HISTORYID)
    )

    create table RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS (
        PPD_RENEWACTIONID INT not null,
       ACTIONTYPE NVARCHAR(255) not null,
       PRIORITY INT null,
       CONFIG_VALUE1 NVARCHAR(255) null,
       CONFIG_VALUE2 NVARCHAR(255) null,
       PRE_PROMOTIONPLAN_DETAILID INT null,
       POST_PROMOTIONPLAN_DETAILID INT null,
       primary key (PPD_RENEWACTIONID)
    )

    create table CRM_CUSTOMERS_PROMOTION_LIMITS (
        PROMOTION_LIMITID BIGINT not null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       INITIALLIMIT DECIMAL(19,5) null,
       CURRENTLIMIT DECIMAL(19,5) null,
       PROMOTIONID BIGINT null,
       primary key (PROMOTION_LIMITID)
    )

    create table dbo.RM_PROMOTIONGROUP_THRESHOLDS (
        THRESHOLDID INT not null,
       THRESHOLDTYPE NVARCHAR(255) null,
       THRESHOLDVALUE DECIMAL(19,5) null,
       THRESHOLDDIRECTION NVARCHAR(255) null,
       GENERATE_EVENTID INT null,
       PROMOTIONGROUPID INT null,
       primary key (THRESHOLDID)
    )

    create table dbo.SYS_NPM_DEALER (
        ID INT not null,
       DealerID INT null,
       ShareType INT null,
       Resource VARCHAR(25) null,
       primary key (ID)
    )

    create table dbo.SYS_NPM (
        Resource VARCHAR(25) not null,
       TrafficTypeID INT null,
       CategoryID INT null,
       UpdateUserID INT null,
       UpdateDate DATETIME null,
       CreateUserID INT null,
       CreateDate DATETIME null,
       DataStatus INT null,
       primary key (Resource)
    )

    create table dbo.SYS_NPM_PROPERTY (
        Resource VARCHAR(25) not null,
       HideCLI BIT null,
       StatusID INT null,
       CoolDownPeriod INT null,
       CoolDownDurDate DATETIME null,
       LockedBy INT null,
       LockedDate DATETIME null,
       TariffCode NVARCHAR(15) null,
       ChangeStatusDate DATETIME null,
       CreateUserID INT null,
       CreateDate DATETIME null,
       UpdateUserID INT null,
       UpdateDate DATETIME null,
       DataStatus INT null,
       locationid NVARCHAR(25) null,
       NUMBERTYPEID NVARCHAR(4) null,
       primary key (Resource)
    )

    create table CRM_MOBILE_SIMCARDS (
        ICCID VARCHAR(20) not null,
       IMSI1 NVARCHAR(255) null,
       IMSI2 NVARCHAR(255) null,
       IMSI3 NVARCHAR(255) null,
       IMSI4 NVARCHAR(255) null,
       IMSI5 NVARCHAR(255) null,
       IMSI6 NVARCHAR(255) null,
       IMSI7 NVARCHAR(255) null,
       IMSI8 NVARCHAR(255) null,
       IMSI9 NVARCHAR(255) null,
       IMSI10 NVARCHAR(255) null,
       IMSI11 NVARCHAR(255) null,
       IMSI12 NVARCHAR(255) null,
       IMSI13 NVARCHAR(255) null,
       IMSI14 NVARCHAR(255) null,
       IMSI15 NVARCHAR(255) null,
       MSISDN NVARCHAR(255) null,
       PIN1 NVARCHAR(255) null,
       PIN2 NVARCHAR(255) null,
       PUK1 NVARCHAR(255) null,
       PUK2 NVARCHAR(255) null,
       KI NVARCHAR(255) null,
       OPC NVARCHAR(255) null,
       KIC_0F NVARCHAR(255) null,
       KID_0F NVARCHAR(255) null,
       KIK_0F NVARCHAR(255) null,
       ADM1 NVARCHAR(255) null,
       ADM2 NVARCHAR(255) null,
       ACC NVARCHAR(255) null,
       STATUS INT null,
       ALGORITHMNAME NVARCHAR(255) null,
       MANUFACTURERID NVARCHAR(255) null,
       SIMTYPE INT null,
       ACTIVATETYPE INT null,
       MANUFACTUREDATE DATETIME null,
       CHANGESTATUSDATE DATETIME null,
       ASSIGNSTATUSID INT null,
       KIC2 NVARCHAR(255) null,
       KID2 NVARCHAR(255) null,
       KIK2 NVARCHAR(255) null,
       AlgoID INT null,
       TEMPORARY_IMSI NVARCHAR(255) null,
       TEMPORARY_MSISDN NVARCHAR(255) null,
       MANUFACTURERENCRYPTIONTYPE INT null,
       DEALERID INT null,
       primary key (ICCID)
    )

    create table dbo.CRM_ACCOUNTS (
        ACCOUNTID BIGINT not null,
       BALANCE_UNITDIMENSIONID INT not null,
       NAMEID INT null,
       DESCRIPTIONID INT null,
       BILLCYCLEID INT null,
       PRIMARY_CUSTOMERID INT null,
       LAST_BILLRUNID INT null,
       BALANCE_UNITTYPEID INT null,
       primary key (ACCOUNTID)
    )

    create table dbo.CRM_ACCOUNT_BALANCES (
        ACCOUNTID BIGINT not null,
       BALANCE_QTY DECIMAL(19,5) null,
       primary key (ACCOUNTID)
    )

    create table dbo.RM_BILL_CYCLES (
        BILLCYCLEID INT IDENTITY NOT NULL,
       BILLRUNDAY SMALLINT null,
       CUTOFFDAY SMALLINT null,
       PERIOD_UNIT NVARCHAR(255) null,
       PERIOD_QUANTITY INT null,
       DAYS_UNTIL_LATE INT null,
       MVNOID INT null,
       DESCRIPTIONID INT null,
       primary key (BILLCYCLEID)
    )

    create table dbo.RM_BILL_RUN (
        BILLRUNID INT not null,
       RUNDATE DATETIME null,
       STARTDATE DATE null,
       ENDDATE DATE null,
       DUEDATE DATE null,
       START_USAGE_DETAILID BIGINT null,
       END_USAGE_DETAILID BIGINT null,
       CUTOFF_DATE DATETIME null,
       BILLCYCLEID INT null,
       primary key (BILLRUNID)
    )

    create table dbo.RM_CHARGES (
        CHARGEID INT not null,
       CHARGETYPEID INT not null,
       CHARGE_CATEGORYID INT null,
       PRORATE_UNITID INT null,
       PRORATE_QTY DECIMAL(19,5) null,
       CREATEDATE DATETIME null,
       INFORMATIONAL_ONLY NVARCHAR(255) null,
       STATUSID TINYINT null,
       TIME_OF_CHARGE NVARCHAR(255) null,
       GL_CODE NVARCHAR(255) null,
       NAMEID INT null,
       DESCRIPTIONID INT null,
       ADJUSTMENTTYPEID INT null,
       ADJUSTMENT_UNITID INT null,
       ADJUSTMENT_QUANTITY DECIMAL(19,5) null,
       PERIOD_UNITID INT null,
       PERIOD_UNIT_COUNT BIGINT null,
       START_PERIODNUMBER INT null,
       END_PERIODNUMBER INT null,
       PERIODICITY INT null,
       CYCLE_REPEAT_COUNT INT null,
       PERIOD_COMMITMENT INT null,
       primary key (CHARGEID)
    )

    create table RM_CHARGE_OPTIONS (
        CHARGEID INT not null,
       PRODUCT_CHARGE_OPTIONID INT not null
    )

    create table dbo.RM_CHARGE_PRICES (
        CHARGEID INT not null,
       STARTDATE DATETIME not null,
       ENDDATE DATETIME null,
       AMOUNT DECIMAL(19,5) null,
       ISO4217_CURRENCY_CODE NVARCHAR(255) null,
       primary key (CHARGEID, STARTDATE)
    )

    create table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES (
        CUSTOMER_CHARGE_SCHEDULEID BIGINT not null,
       NEXT_CHARGE_DATE DATETIME null,
       NEXT_PERIOD_NUMBER INT null,
       CURRENT_CYCLE_NUMBER INT null,
       PRICE_EFFECTIVE_DATE DATETIME null,
       CREATEDATE DATETIME null,
       UPDATEDATE DATETIME null,
       STATUSID INT null,
       CHARGEID INT null,
       CUSTOMERID INT null,
       ACCOUNTID BIGINT null,
       CUSTOMERPRODUCTID BIGINT null,
       primary key (CUSTOMER_CHARGE_SCHEDULEID)
    )

    create table dbo.CRM_CUSTOMER_PRODUCT_ASSN (
        CUSTOMERPRODUCTID BIGINT not null,
       STARTDATE DATETIME null,
       CREATEDATE DATETIME null,
       ENDDATE DATETIME null,
       CUSTOMERID INT null,
       PRODUCTGK INT null,
       PRODUCT_CHARGE_OPTIONID INT null,
       primary key (CUSTOMERPRODUCTID)
    )

    create table RM_CHARGE_TARGETS (
        TARGETID INT not null,
       COMPUTATION_ORDER INT null,
       TARGET_CHARGE_CATEGORYID INT null,
       CHARGEID INT null,
       TARGET_CHARGEID INT null,
       primary key (TARGETID)
    )

    create table dbo.CRM_CUSTOMER_ACCOUNT_ASSN (
        CUSTOMER_ACCOUNT_ASSNID INT not null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       ACCOUNTID BIGINT null,
       CUSTOMERID INT null,
       primary key (CUSTOMER_ACCOUNT_ASSNID)
    )

    create table dbo.CRM_CUSTOMERS_CHARGES (
        CUSTOMER_CHARGEID BIGINT not null,
       ISO4217_CURRENCY_CODE INT null,
       AMOUNT DECIMAL(19,5) null,
       INFORMATIONAL_AMOUNT DECIMAL(19,5) null,
       CHARGEDATE DATETIME null,
       PERIODNUMBER BIGINT null,
       CYCLE_NUMBER BIGINT null,
       COMMENTS NVARCHAR(255) null,
       EXTERNAL_REFERENCE_CODE NVARCHAR(255) null,
       TAX_AMOUNT DECIMAL(19,5) null,
       CUSTOMERID INT null,
       ACCOUNTID BIGINT null,
       CUSTOMERPRODUCTID BIGINT null,
       CUSTOMER_CHARGE_SCHEDULEID BIGINT null,
       CHARGEID INT null,
       INVOICEID BIGINT null,
       TAXID INT null,
       primary key (CUSTOMER_CHARGEID)
    )

    create table dbo.CRM_ACCOUNT_INVOICES (
        INVOICEID BIGINT not null,
       LEGAL_INVOICE_NUMBER NVARCHAR(255) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       STATUSID INT null,
       INVOICE_FILENAME NVARCHAR(255) null,
       ACCOUNTID BIGINT null,
       CUSTOMERID INT null,
       BILLRUNID INT null,
       primary key (INVOICEID)
    )

    create table dbo.RM_PRODUCT_RELATIONS (
        PRODUCT_RELATIONID INT not null,
       MAX_OCCURS INT null,
       MIN_OCCURS INT null,
       RELATION_TYPE NVARCHAR(255) null,
       CONFLICT_STRATEGY NVARCHAR(255) null,
       PRODUCTGK INT null,
       RELATED_PRODUCTGK INT null,
       primary key (PRODUCT_RELATIONID)
    )

    create table dbo.PRODUCTTYPES (
        PRODUCTTYPEID INT not null,
       DESCRIPTION NVARCHAR(255) null,
       primary key (PRODUCTTYPEID)
    )

    create table dbo.TAXES (
        TAXID INT not null,
       TAX_CATEGORYID INT null,
       DESCRIPTIONID INT null,
       primary key (TAXID)
    )

    create table dbo.TAX_RATES (
        TAX_RATEID INT not null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       TAX_PERCENTAGE DECIMAL(19,5) null,
       TAXID INT null,
       primary key (TAX_RATEID)
    )

    create table dbo.TAX_ZIPCODE_RANGES (
        TAX_ZIPCODE_RANGEID INT not null,
       ZIPCODE_HIGH NVARCHAR(255) null,
       ZIPCODE_LOW NVARCHAR(255) null,
       TAXID INT null,
       primary key (TAX_ZIPCODE_RANGEID)
    )

    create table dbo.PRODUCT_CHARGE_OPTIONS (
        PRODUCT_CHARGE_OPTIONID INT not null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       CREATEDATE DATETIME null,
       STATUSID INT null,
       IS_DEFAULT NVARCHAR(255) null,
       PRODUCTGK INT null,
       NAMEID INT null,
       DESCRIPTIONID INT null,
       primary key (PRODUCT_CHARGE_OPTIONID)
    )

    create table dbo.RM_PRODUCTS (
        PRODUCTGK INT not null,
       STATUS NVARCHAR(255) null,
       PRODUCTTYPEID INT null,
       NAMEID INT null,
       DESCRIPTIONID INT null,
       MVNOID INT null,
       PACKAGEID INT null,
       BUNDLEID INT null,
       PROMOTIONGROUPID INT null,
       PROMOTIONPLANID INT null,
       primary key (PRODUCTGK)
    )

    create table RM_PRODUCT_GROUPS (
        PRODUCTGK INT not null,
       CHILD_PRODUCTGK INT not null
    )

    create table CRM_BUSINESSRULES (
        Id INT not null,
       ClassType NVARCHAR(255) not null,
       Name NVARCHAR(255) null,
       JSONConfig NVARCHAR(255) null,
       primary key (Id)
    )

    create table CRM_BUSINESSRULES_PACKAGES (
        Id INT not null,
       PACKAGEID INT null,
       RULEID INT null,
       primary key (Id)
    )

    create table dbo.USAGE_DETAIL (
        usage_detail_id BIGINT not null,
       IMEI NVARCHAR(255) null,
       PROMOTIONLIMIT DECIMAL(19,5) null,
       PROMOTIONID BIGINT null,
       Prompt DECIMAL(19,5) null,
       PROMOTIONPLANDETAILID INT null,
       PROMOTIONPLANID INT null,
       GSNIPV4 NVARCHAR(255) null,
       TSC NVARCHAR(255) null,
       TSP NVARCHAR(255) null,
       BNUMBERADDRESS NVARCHAR(255) null,
       ROAMINGMSCNUMBER NVARCHAR(255) null,
       ROAMINGZONE2 INT null,
       ROAMINGZONE1 INT null,
       PROVIDERID INT null,
       SETUP DECIMAL(19,5) null,
       TARIFF2 DECIMAL(19,5) null,
       TARIFF1 DECIMAL(19,5) null,
       APN NVARCHAR(255) null,
       CALLTYPEID INT null,
       CALLDIRECTIONID INT null,
       TIMECATEGORYID INT null,
       UNITCATEGORYID INT null,
       CURRENCYID INT null,
       RATETYPEID INT null,
       IMSI NVARCHAR(255) null,
       AMOUNT1 DECIMAL(19,5) null,
       AMOUNT2 DECIMAL(19,5) null,
       ALEG DECIMAL(19,5) null,
       BLEG DECIMAL(19,5) null,
       RATEPLANID INT null,
       RATEPLANDETAILID INT null,
       COUNTRYCODE NVARCHAR(255) null,
       SERVICETYPEID INT null,
       SUBSERVICETYPEID INT null,
       ANUMBER NVARCHAR(255) null,
       BNUMBER NVARCHAR(255) null,
       CNUMBER NVARCHAR(255) null,
       STARTDATE DATETIME null,
       ENDDATE DATETIME null,
       primary key (usage_detail_id)
    )

    alter table CRM_CUSTOMERS_BANK 
        add constraint FK11E386F5918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_MVNO_BRS_TAXCODE_POSTCODE 
        add constraint FK2804FD15EED2B984 
        foreign key (TAXCODE) 
        references CRM_MVNO_BRS_TAXCODE

    alter table RM_BUNDLE_PACKAGE 
        add constraint FK8A4264688CAF124E 
        foreign key (PackageID) 
        references RM_PACKAGE

    alter table CRM_CUSTOMERS_PROMOTIONGROUPS 
        add constraint FK1798439F9B7C8FC 
        foreign key (PROMOTIONGROUPID) 
        references RM_PROMOTIONGROUPS

    alter table CRM_CUSTOMERS_PROMOTIONGROUPS 
        add constraint FK1798439F918EF537 
        foreign key (CustomerID) 
        references CRM_CUSTOMERS

    alter table CRM_CUSTOMERS_PROMOTION 
        add constraint FK7D536073918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_CUSTOMERS_PROMOTION 
        add constraint FK7D53607381DC76C9 
        foreign key (PROMOTIONPLANDETAILID) 
        references RM_PROMOTIONPLAN_DETAIL

    alter table CRM_CUSTOMERS_RESOURCEMB 
        add constraint FK9BC81200918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_CUSTOMERS_RESOURCEMB 
        add constraint FK9BC81200240E3AD 
        foreign key (OperatorCode) 
        references CRM_DEALERS

    alter table CRM_CUSTOMERS_RESOURCEMB_PROPERTY 
        add constraint FK482EEC20C3631F53 
        foreign key (RESOURCEID) 
        references CRM_CUSTOMERS_RESOURCEMB

    alter table CRM_CUSTOMERS_UNPAIDFEE 
        add constraint FK2DFA0D858E62C593 
        foreign key (PromotionPlanId) 
        references RM_PROMOTIONPLAN

    alter table CRM_CUSTOMERS_CREDITCARDS 
        add constraint FKD9B256F4918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_CUSTOMERS_DATA_ROAMING_LIMIT 
        add constraint FKB9AF7D7D918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_CUSTOMERS_DATA_ROAMING_LIMIT_NOTIFICATION 
        add constraint FKC98644EA918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_CUSTOMERS_DATA_ROAMING_LIMIT_NOTIFICATION 
        add constraint FKC98644EA62CB716B 
        foreign key (MVNONOTIFICATIONID) 
        references CRM_MVNO_DATA_ROAMING_LIMIT_NOTIFICATION

    alter table CRM_CUSTOMERS_ADDRESSES 
        add constraint FKAE0C9D6A1B2C3B3 
        foreign key (ADDRESSID) 
        references dbo.ADDRESSES

    alter table CRM_CUSTOMERS_ADDRESSES 
        add constraint FKAE0C9D6A918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_MVNO_TOPUP_CONFIG 
        add constraint FK72F57464E681CE1B 
        foreign key (DEALERID) 
        references CRM_DEALERS

    alter table CRM_DEALERS_BANK 
        add constraint FKF1C3133EE681CE1B 
        foreign key (DealerID) 
        references CRM_DEALERS

    alter table CRM_DEALERS_LOYALTY_POINT 
        add constraint FKC95970FCE681CE1B 
        foreign key (DealerID) 
        references CRM_DEALERS

    alter table CRM_DEALERS_PROPERTIES 
        add constraint FK38A05257E681CE1B 
        foreign key (DealerID) 
        references CRM_DEALERS

    alter table CRM_CUSTOMERS_DID 
        add constraint FK71B39DC56D277E12 
        foreign key (RESOURCEID) 
        references CRM_CUSTOMERS_RESOURCEMB

    alter table CRM_EMAIL_ATTACHMENT_HISTORY 
        add constraint FKD3E349DD83C86550 
        foreign key (EmailId) 
        references CRM_EMAIL_HISTORY

    alter table RM_INTERNATIONAL_ZONE_DETAILS 
        add constraint FKFFD9BC93A6340610 
        foreign key (ZoneID) 
        references RM_INTERNATIONAL_ZONE

    alter table MVNO_FC_LIFECYCLE_LOG_EXTEND 
        add constraint FK961F21D8EDCB41EE 
        foreign key (LOGID) 
        references MVNO_FC_LIFECYCLE_LOG

    alter table CRM_CUSTOMERS_MAPPING 
        add constraint FK47B4C79A918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_MVNO_CONFIGACTION 
        add constraint FK9B65EF3BE681CE1B 
        foreign key (DealerID) 
        references CRM_DEALERS

    alter table CRM_MVNO_CUSTOMERS_PROPERTY 
        add constraint FKE188EF2F918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_MVNO_DATA_ROAMING_LIMIT 
        add constraint FKDB9DEDCADD7BC768 
        foreign key (MVNOID) 
        references CRM_DEALERS

    alter table CRM_MVNO_DATA_ROAMING_LIMIT_NOTIFICATION 
        add constraint FK24095A4DD7BC768 
        foreign key (MVNOID) 
        references CRM_DEALERS

    alter table CRM_MVNO_PROPERTIES 
        add constraint FK39AD6064E681CE1B 
        foreign key (DEALERID) 
        references CRM_DEALERS

    alter table CRM_MVNO_OPERATION_DETAILS_LOG 
        add constraint FKB12B7BCD7E5EB055 
        foreign key (OrderCode) 
        references CRM_MVNO_OPERATION_LOG

    alter table CRM_MVNO_OPERATION_LOG 
        add constraint FK5758805C7BDE710E 
        foreign key (LogID) 
        references SYS25_LOG_OPERATION

    alter table CRM_MVNO_OPERATION_LOG 
        add constraint FK5758805C1624A2CC 
        foreign key (ID) 
        references SYS25_LOG_OPERATION

    alter table RM_PACKAGE_RELATIONSHIPS 
        add constraint FK8D729B2BB67DA44 
        foreign key (ORIGINPACKAGEID) 
        references RM_PACKAGE

    alter table RM_PACKAGE_RELATIONSHIPS 
        add constraint FK8D729B2B76B64A25 
        foreign key (TARGETPACKAGEID) 
        references RM_PACKAGE

    alter table CRM_CUSTOMERS_PRODUCTS 
        add constraint FKA4BE07FF918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_CUSTOMERS_PRODUCTS 
        add constraint FKA4BE07FF8CAF124E 
        foreign key (PackageID) 
        references RM_PACKAGE

    alter table CRM_CUSTOMERS_PROPERTY 
        add constraint FKA6B3BE4F918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table RM_PROMOTIONGROUP_MEMBERS 
        add constraint FKE018525D9B7C8FC 
        foreign key (PROMOTIONGROUPID) 
        references RM_PROMOTIONGROUPS

    alter table RM_PROMOTIONGROUP_MEMBERS 
        add constraint FKE018525D8E62C593 
        foreign key (PROMOTIONPLANID) 
        references RM_PROMOTIONPLAN

    alter table RM_PROMOTIONPLAN_DETAIL 
        add constraint FK7C5260A88E62C593 
        foreign key (PROMOTIONPLANID) 
        references RM_PROMOTIONPLAN

    alter table RM_PROMOTIONPLAN_RULE 
        add constraint FK1AE8C8498E62C593 
        foreign key (PROMOTIONPLANID) 
        references RM_PROMOTIONPLAN

    alter table RM_SPECIFICNUMBER_GROUP 
        add constraint FK245B0A718E62C593 
        foreign key (PROMOTIONPLANID) 
        references RM_PROMOTIONPLAN

    alter table CRM_ROAMING_SETTING 
        add constraint FKDADDF87E681CE1B 
        foreign key (DealerID) 
        references CRM_DEALERS

    alter table RM_ROAMING_ZONE_DETAILS 
        add constraint FK42540FC72CC1C7F0 
        foreign key (ZoneID) 
        references RM_ROAMING_ZONE

    alter table CRM_CUSTOMERS_SERVICES 
        add constraint FKAD691CA4672541E2 
        foreign key (ProductID) 
        references CRM_CUSTOMERS_PRODUCTS

    alter table CRM_CUSTOMERS_SERVICES 
        add constraint FKAD691CA4918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_CUSTOMERS_SERVICES 
        add constraint FKAD691CA4467CA9B 
        foreign key (BUNDLEID) 
        references RM_BUNDLE

    alter table CRM_LIFECYCLE_SETTINGS_DETAILS 
        add constraint FK94A90C99DC34746B 
        foreign key (SettingId) 
        references CRM_LIFECYCLE_SETTINGS

    alter table CRM_LIFECYCLE_SETTINGS 
        add constraint FK9230C4A4E5A56660 
        foreign key (EventId) 
        references CRM_LIFECYCLE_EVENTS

    alter table CRM_LIFECYCLE_SMS_TEMPLETS 
        add constraint FK8C3923A3DC34746B 
        foreign key (SettingId) 
        references CRM_LIFECYCLE_SETTINGS

    alter table SYS25_USER_DEALER 
        add constraint FK1D084411B7AD2A0A 
        foreign key (UserID) 
        references SYS25_USER

    alter table CRM_DEALERS_OBOPRS 
        add constraint FK8962D8ADE681CE1B 
        foreign key (DealerID) 
        references CRM_DEALERS

    alter table CRM_MOBILE_MULTIPLEIMSI 
        add constraint FK82A80B36C3631F53 
        foreign key (RESOURCEID) 
        references CRM_CUSTOMERS_RESOURCEMB

    alter table CRM_MOBILE_NETWORK 
        add constraint FK2533C01FC3631F53 
        foreign key (RESOURCEID) 
        references CRM_CUSTOMERS_RESOURCEMB

    alter table CRM_DEFAULT_PROVISION_NETWORK 
        add constraint FK7A5E7F7E8F47D972 
        foreign key (PROVISIONID) 
        references CRM_DEFAULT_PROVISION

    alter table CRM_DEFAULT_PROVISION_ROAMING_SETTING 
        add constraint FK6A144CEBE681CE1B 
        foreign key (DealerId) 
        references CRM_DEALERS

    alter table CRM_CUSTOMERS_REMARKS 
        add constraint FK49611DE3918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_BUSINESSRULES_PROMOTIONGROUPS 
        add constraint FKF3F9627F6D1EF1B1 
        foreign key (PromotionGroupID ) 
        references RM_PROMOTIONGROUPS

    alter table CRM_BUSINESSRULES_PROMOTIONGROUPS 
        add constraint FKF3F9627FD67CDE98 
        foreign key (RuleID) 
        references CRM_BUSINESSRULES

    alter table CRM_BUSINESSRULES_PROMOTIONGROUPS 
        add constraint FKF3F9627F9B7C8FC 
        foreign key (PromotionGroupID) 
        references RM_PROMOTIONGROUPS

    alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY 
        add constraint FK_CRM_CUSTOMER_SERVICESID 
        foreign key (SERVICEID) 
        references CRM_CUSTOMERS_SERVICES

    alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY 
        add constraint FK_CRM_CUSTOMER_PROMOTIONSID 
        foreign key (PROMOTIONID) 
        references CRM_CUSTOMERS_PROMOTION

    alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY 
        add constraint FK_CRM_OPERATION_EXECUTIONSID 
        foreign key (OPERATION_EXECUTIONID) 
        references CRM_OPERATION_EXECUTIONS

    alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY 
        add constraint FK_CRM_CUSTOMERSID 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY 
        add constraint FK_RM_PROMOTIONPLANID 
        foreign key (PROMOTIONPLANID) 
        references RM_PROMOTIONPLAN

    alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY 
        add constraint FK_CRM_CUSTOMER_RESOURCEMBID 
        foreign key (RESOURCEID) 
        references CRM_CUSTOMERS_RESOURCEMB

    alter table CRM_CUSTOMERS_BALANCE_TRANSACTION_HISTORY 
        add constraint FK_RM_PACKAGES 
        foreign key (PACKAGEID) 
        references RM_PACKAGE

    alter table dbo.TEXT_LOOKUP_TRANSLATIONS 
        add constraint FK514F7399C39BC787 
        foreign key (LOOKUPID) 
        references dbo.TEXT_LOOKUPS

    create index IX_CUSTOMERID on CRM_OPERATION_EXECUTIONS (CUSTOMERID)

    create index IX_DEST_CUSTOMERID on CRM_OPERATION_EXECUTIONS (DESTINATION_CUSTOMERID)

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK1793C4226531341 
        foreign key (OPERATIONID) 
        references CRM_OPERATIONS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK1793C42239883E47 
        foreign key (OPERATION_CONFIGURATIONID) 
        references CRM_OPERATION_CONFIGURATIONS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_PARENT_OPERATIONID 
        foreign key (PARENT_OPERATION_EXECUTIONID) 
        references CRM_OPERATION_EXECUTIONS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_ROOT_OPEXECUTIONID 
        foreign key (ROOT_OPERATION_EXECUTIONID) 
        references CRM_OPERATION_EXECUTIONS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_USERID 
        foreign key (USERID) 
        references SYS25_USER

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_DEALERID 
        foreign key (FISCALUNITID) 
        references CRM_DEALERS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_CUSTOMERID 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_DESTINATION_CUSTOMER_ID 
        foreign key (DESTINATION_CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_RESOURCEMBID 
        foreign key (RESOURCEID) 
        references CRM_CUSTOMERS_RESOURCEMB

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_DESTINATION_RESOURCEMBID 
        foreign key (DESTINATION_RESOURCEID) 
        references CRM_CUSTOMERS_RESOURCEMB

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_SYS_NPMID 
        foreign key (MSISDN) 
        references dbo.SYS_NPM

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_ACCOUNTID 
        foreign key (ACCOUNTID) 
        references dbo.CRM_ACCOUNTS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERATION_PRODUCTID 
        foreign key (PRODUCTGK) 
        references dbo.RM_PRODUCTS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_CUST_PROD_ASSNID 
        foreign key (CUSTOMERPRODUCTID) 
        references dbo.CRM_CUSTOMER_PRODUCT_ASSN

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERAION_SIMID 
        foreign key (ICCID) 
        references CRM_MOBILE_SIMCARDS

    alter table CRM_OPERATION_EXECUTIONS 
        add constraint FK_OPERATION_ORDER_ID 
        foreign key (ORDERID) 
        references CRM_ORDERS

    alter table CRM_OPERATIONS 
        add constraint FK_TEXT_LOOKUP 
        foreign key (DESCRIPTIONID) 
        references dbo.TEXT_LOOKUPS

    alter table CRM_OPERATION_CONFIGURATIONS 
        add constraint FK_CONFIG_DEALER 
        foreign key (DEALERID) 
        references CRM_DEALERS

    alter table CRM_OPERATION_CONFIGURATIONS 
        add constraint FK_OPERATIONID 
        foreign key (OPERATIONID) 
        references CRM_OPERATIONS

    alter table CRM_ORDERS 
        add constraint FK_ORDER_DEALERID 
        foreign key (FISCALUNITID) 
        references CRM_DEALERS

    alter table CRM_ORDERS 
        add constraint FK_PARENT_ORDER_ID 
        foreign key (PARENT_ORDERID) 
        references CRM_ORDERS

    alter table CRM_ORDERS 
        add constraint FK_ORDER_OPERATION_ID 
        foreign key (STARTING_OPERATIONEXECUTIONID) 
        references CRM_OPERATION_EXECUTIONS

    alter table CRM_ORDER_STATUS_HISTORY 
        add constraint FK_ORDER_TRANSITION_ORDERID 
        foreign key (ORDERID) 
        references CRM_ORDERS

    alter table CRM_ORDER_STATUS_HISTORY 
        add constraint FK_TRANSITION_OPERATION_EXECUTIONID 
        foreign key (OPERATION_EXECUTIONID) 
        references CRM_OPERATION_EXECUTIONS

    alter table RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS 
        add constraint FK78C2F32781C64F30 
        foreign key (PRE_PROMOTIONPLAN_DETAILID) 
        references RM_PROMOTIONPLAN_DETAIL

    alter table RM_PROMOTIONPLAN_DETAIL_RENEW_ACTIONS 
        add constraint FK78C2F3272BB74448 
        foreign key (POST_PROMOTIONPLAN_DETAILID) 
        references RM_PROMOTIONPLAN_DETAIL

    alter table CRM_CUSTOMERS_PROMOTION_LIMITS 
        add constraint FKC8F35322A9D67797 
        foreign key (PROMOTIONID) 
        references CRM_CUSTOMERS_PROMOTION

    alter table dbo.RM_PROMOTIONGROUP_THRESHOLDS 
        add constraint FKE150D139B7C8FC 
        foreign key (PROMOTIONGROUPID) 
        references RM_PROMOTIONGROUPS

    alter table dbo.SYS_NPM_DEALER 
        add constraint FKBDC1122364773DF 
        foreign key (Resource) 
        references dbo.SYS_NPM

    alter table dbo.SYS_NPM_PROPERTY 
        add constraint FK5645BBB064773DF 
        foreign key (Resource) 
        references dbo.SYS_NPM

    alter table CRM_MOBILE_SIMCARDS 
        add constraint FK76C3DB70E681CE1B 
        foreign key (DEALERID) 
        references CRM_DEALERS

    alter table dbo.CRM_ACCOUNTS 
        add constraint FKEE721C555C0F5C8 
        foreign key (NAMEID) 
        references dbo.TEXT_LOOKUPS

    alter table dbo.CRM_ACCOUNTS 
        add constraint FKEE721C553370A309 
        foreign key (DESCRIPTIONID) 
        references dbo.TEXT_LOOKUPS

    alter table dbo.CRM_ACCOUNTS 
        add constraint FKEE721C55B83192F2 
        foreign key (BILLCYCLEID) 
        references dbo.RM_BILL_CYCLES

    alter table dbo.CRM_ACCOUNTS 
        add constraint FKEE721C557EBB0BF4 
        foreign key (PRIMARY_CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table dbo.CRM_ACCOUNTS 
        add constraint FKEE721C5585F3D114 
        foreign key (LAST_BILLRUNID) 
        references dbo.RM_BILL_RUN

    alter table dbo.CRM_ACCOUNT_BALANCES 
        add constraint FK4B14693C52514B24 
        foreign key (ACCOUNTID) 
        references dbo.CRM_ACCOUNTS

    alter table dbo.RM_BILL_CYCLES 
        add constraint FK2DD05787DD7BC768 
        foreign key (MVNOID) 
        references CRM_DEALERS

    alter table dbo.RM_BILL_CYCLES 
        add constraint FK2DD057873370A309 
        foreign key (DESCRIPTIONID) 
        references dbo.TEXT_LOOKUPS

    alter table dbo.RM_BILL_RUN 
        add constraint FK31B5AA38B83192F2 
        foreign key (BILLCYCLEID) 
        references dbo.RM_BILL_CYCLES

    alter table dbo.RM_CHARGES 
        add constraint FK25382FA05C0F5C8 
        foreign key (NAMEID) 
        references dbo.TEXT_LOOKUPS

    alter table dbo.RM_CHARGES 
        add constraint FK25382FA03370A309 
        foreign key (DESCRIPTIONID) 
        references dbo.TEXT_LOOKUPS

    alter table RM_CHARGE_OPTIONS 
        add constraint FK44A053914D45695C 
        foreign key (PRODUCT_CHARGE_OPTIONID) 
        references dbo.PRODUCT_CHARGE_OPTIONS

    alter table RM_CHARGE_OPTIONS 
        add constraint FK44A0539192203841 
        foreign key (CHARGEID) 
        references dbo.RM_CHARGES

    alter table dbo.RM_CHARGE_PRICES 
        add constraint FKF8C80A8B92203841 
        foreign key (CHARGEID) 
        references dbo.RM_CHARGES

    alter table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES 
        add constraint FKA2F2647192203841 
        foreign key (CHARGEID) 
        references dbo.RM_CHARGES

    alter table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES 
        add constraint FKA2F26471918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES 
        add constraint FKA2F2647152514B24 
        foreign key (ACCOUNTID) 
        references dbo.CRM_ACCOUNTS

    alter table dbo.CRM_CUSTOMER_CHARGE_SCHEDULES 
        add constraint FKA2F26471DB8EF603 
        foreign key (CUSTOMERPRODUCTID) 
        references dbo.CRM_CUSTOMER_PRODUCT_ASSN

    alter table dbo.CRM_CUSTOMER_PRODUCT_ASSN 
        add constraint FK1C145B79918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table dbo.CRM_CUSTOMER_PRODUCT_ASSN 
        add constraint FK1C145B79A624DED6 
        foreign key (PRODUCTGK) 
        references dbo.RM_PRODUCTS

    alter table dbo.CRM_CUSTOMER_PRODUCT_ASSN 
        add constraint FK1C145B794D45695C 
        foreign key (PRODUCT_CHARGE_OPTIONID) 
        references dbo.PRODUCT_CHARGE_OPTIONS

    alter table RM_CHARGE_TARGETS 
        add constraint FKBD5506E092203841 
        foreign key (CHARGEID) 
        references dbo.RM_CHARGES

    alter table RM_CHARGE_TARGETS 
        add constraint FKBD5506E016F0D4F9 
        foreign key (TARGET_CHARGEID) 
        references dbo.RM_CHARGES

    alter table dbo.CRM_CUSTOMER_ACCOUNT_ASSN 
        add constraint FKA59E560752514B24 
        foreign key (ACCOUNTID) 
        references dbo.CRM_ACCOUNTS

    alter table dbo.CRM_CUSTOMER_ACCOUNT_ASSN 
        add constraint FKA59E5607918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table dbo.CRM_CUSTOMERS_CHARGES 
        add constraint FK2E2E8323918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table dbo.CRM_CUSTOMERS_CHARGES 
        add constraint FK2E2E832352514B24 
        foreign key (ACCOUNTID) 
        references dbo.CRM_ACCOUNTS

    alter table dbo.CRM_CUSTOMERS_CHARGES 
        add constraint FK2E2E8323DB8EF603 
        foreign key (CUSTOMERPRODUCTID) 
        references dbo.CRM_CUSTOMER_PRODUCT_ASSN

    alter table dbo.CRM_CUSTOMERS_CHARGES 
        add constraint FK2E2E8323DAFB36F4 
        foreign key (CUSTOMER_CHARGE_SCHEDULEID) 
        references dbo.CRM_CUSTOMER_CHARGE_SCHEDULES

    alter table dbo.CRM_CUSTOMERS_CHARGES 
        add constraint FK2E2E832392203841 
        foreign key (CHARGEID) 
        references dbo.RM_CHARGES

    alter table dbo.CRM_CUSTOMERS_CHARGES 
        add constraint FK2E2E83236D30F8FE 
        foreign key (INVOICEID) 
        references dbo.CRM_ACCOUNT_INVOICES

    alter table dbo.CRM_CUSTOMERS_CHARGES 
        add constraint FK2E2E8323B666BB6F 
        foreign key (TAXID) 
        references dbo.TAXES

    alter table dbo.CRM_ACCOUNT_INVOICES 
        add constraint FK4431F07A52514B24 
        foreign key (ACCOUNTID) 
        references dbo.CRM_ACCOUNTS

    alter table dbo.CRM_ACCOUNT_INVOICES 
        add constraint FK4431F07A918EF537 
        foreign key (CUSTOMERID) 
        references CRM_CUSTOMERS

    alter table dbo.CRM_ACCOUNT_INVOICES 
        add constraint FK4431F07A57DDFD24 
        foreign key (BILLRUNID) 
        references dbo.RM_BILL_RUN

    alter table dbo.RM_PRODUCT_RELATIONS 
        add constraint FK4A68CAC8A624DED6 
        foreign key (PRODUCTGK) 
        references dbo.RM_PRODUCTS

    alter table dbo.RM_PRODUCT_RELATIONS 
        add constraint FK4A68CAC8AC50E958 
        foreign key (RELATED_PRODUCTGK) 
        references dbo.RM_PRODUCTS

    alter table dbo.TAXES 
        add constraint FK80A8B6B53370A309 
        foreign key (DESCRIPTIONID) 
        references dbo.TEXT_LOOKUPS

    alter table dbo.TAX_RATES 
        add constraint FKC8105EBFB666BB6F 
        foreign key (TAXID) 
        references dbo.TAXES

    alter table dbo.TAX_ZIPCODE_RANGES 
        add constraint FKD7E96151B666BB6F 
        foreign key (TAXID) 
        references dbo.TAXES

    alter table dbo.PRODUCT_CHARGE_OPTIONS 
        add constraint FK9EB044EAA624DED6 
        foreign key (PRODUCTGK) 
        references dbo.RM_PRODUCTS

    alter table dbo.PRODUCT_CHARGE_OPTIONS 
        add constraint FK9EB044EA5C0F5C8 
        foreign key (NAMEID) 
        references dbo.TEXT_LOOKUPS

    alter table dbo.PRODUCT_CHARGE_OPTIONS 
        add constraint FK9EB044EA3370A309 
        foreign key (DESCRIPTIONID) 
        references dbo.TEXT_LOOKUPS

    alter table dbo.RM_PRODUCTS 
        add constraint FKE626B9BB950FE7FB 
        foreign key (PRODUCTTYPEID) 
        references dbo.PRODUCTTYPES

    alter table dbo.RM_PRODUCTS 
        add constraint FKE626B9BB5C0F5C8 
        foreign key (NAMEID) 
        references dbo.TEXT_LOOKUPS

    alter table dbo.RM_PRODUCTS 
        add constraint FKE626B9BB3370A309 
        foreign key (DESCRIPTIONID) 
        references dbo.TEXT_LOOKUPS

    alter table dbo.RM_PRODUCTS 
        add constraint FKE626B9BBDD7BC768 
        foreign key (MVNOID) 
        references CRM_DEALERS

    alter table dbo.RM_PRODUCTS 
        add constraint FKE626B9BB8CAF124E 
        foreign key (PACKAGEID) 
        references RM_PACKAGE

    alter table dbo.RM_PRODUCTS 
        add constraint FKE626B9BB467CA9B 
        foreign key (BUNDLEID) 
        references RM_BUNDLE

    alter table dbo.RM_PRODUCTS 
        add constraint FKE626B9BB9B7C8FC 
        foreign key (PROMOTIONGROUPID) 
        references RM_PROMOTIONGROUPS

    alter table dbo.RM_PRODUCTS 
        add constraint FKE626B9BB8E62C593 
        foreign key (PROMOTIONPLANID) 
        references RM_PROMOTIONPLAN

    alter table RM_PRODUCT_GROUPS 
        add constraint FK1AB0025C89806304 
        foreign key (CHILD_PRODUCTGK) 
        references dbo.RM_PRODUCTS

    alter table RM_PRODUCT_GROUPS 
        add constraint FK1AB0025CA624DED6 
        foreign key (PRODUCTGK) 
        references dbo.RM_PRODUCTS

    alter table CRM_BUSINESSRULES_PACKAGES 
        add constraint FK3E60F2C8CAF124E 
        foreign key (PACKAGEID) 
        references RM_PACKAGE

    alter table CRM_BUSINESSRULES_PACKAGES 
        add constraint FK3E60F2C235024C6 
        foreign key (RULEID) 
        references CRM_BUSINESSRULES
