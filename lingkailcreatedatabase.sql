/*
TRUNCATE table "Boards" RESTART IDENTITY CASCADE;
TRUNCATE table "Zones" RESTART IDENTITY CASCADE;
TRUNCATE table "Locations" RESTART IDENTITY CASCADE;
TRUNCATE table "Displays" RESTART IDENTITY CASCADE;
TRUNCATE table "EditorMessages" RESTART IDENTITY CASCADE; /*Note: previously GroupMessage*/
--TRUNCATE table "MessageAssignments" RESTART IDENTITY CASCADE;
--TRUNCATE table "Histories" RESTART IDENTITY CASCADE; 

*/

INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 1, 'V001', 'sungeiwang_192x64', 1, 'SUNGEI WANG PLAZA', 'OPEN', 'SGWANG');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 2, 'V001', 'lowyat_192x64', 1, 'PLAZA LOW YAT', '0787', 'LOWYAT');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 3, 'V001', 'Lot10_192x64', 1, 'LOT 10', 'CLOSE', 'LOT10');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 4, 'V001', 'fahrenheit_192x64', 1, 'FAHRENHEIT88', '5555', 'FAHRENHEIT');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 5, 'V001', 'starhill_192x64', 1, 'STARHILL GALLERY', '0659', 'STARHILL');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 6, 'V001', 'pavilion_192x64', 1, 'PAVILION', 'OPEN', 'PAVILION');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 7, 'V001', 'KLCC_192x64', 1, 'KLCC', '6940', 'KLCC');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 8, 'V001', 'semuahouse_192x64', 2, 'SEMUA HOUSE', 'OPEN', 'SEMUAHOUSE');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 9, 'V001', 'pt80_192x64', 2, 'PT80', 'OPEN', 'PT80');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 10, 'V001', 'capsquare_192x64', 2, 'CAPITAL SQUARE', 'OPEN', 'CAPSQUARE');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 11, 'V001', 'pertamacomplex_192x64', 2, 'PERTAMA COMPLEX', 'OPEN', 'PERTAMA');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 12, 'V001', 'sogo_192x64', 2, 'SOGO', 'OPEN', 'SOGO');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 13, 'V001', 'quill_192x64', 2, 'QUILL CITY MALL', 'OPEN', 'QUILLCITY');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 14, 'V001', '', 2, 'NULL', 'OPEN', 'MALL14');
INSERT INTO public."ParkingInfos" ("Board", "NowDateTime", "MallID", "Bname", "ImageFileName", "Phase", name, parking, sname) VALUES ('1', '2020-01-01 12:00:00', 15, 'V001', 'MajuJunction_192x64', 2, 'PARKSON MAJU JUNCTION', 'OPEN', 'MAJUJUNCTION');


INSERT INTO "UptimeReports"("ID","Name","Date","Remark", "TimeEnd", "TimeStart") 
VALUES 
('1','V001','2020-06-01 23:13:14.016994','-','-','09:10 AM'), 
('2','V001','2020-06-15 23:13:14.016994','-','-','09:10 AM'), 
('3','V001','2020-06-30 23:13:14.016994','-','09:45 AM','09:10 AM'),
('4','V002','2020-06-01 23:13:14.016994','-','-','09:10 AM'), 
('5','V002','2020-06-15 23:13:14.016994','-','-','10:09 AM'), 
('6','V002','2020-06-30 23:13:14.016994','-','-','10:09 AM'),
('7','V003','2020-06-01 23:13:14.016994','-','10:16 AM','10:09 AM'), 
('8','V003','2020-06-15 23:13:14.016994','-','-','10:09 AM'), 
('9','V003','2020-06-30 23:13:14.016994','-','-','10:39 AM'), 
('10','V004','2020-06-01 23:13:14.016994','-','-','10:39 AM'), 
('11','V004','2020-06-15 23:13:14.016994','-','-','10:39 AM'), 
('12','V004','2020-06-30 23:13:14.016994','-','-','10:39 AM'), 
('13','V001','2020-07-01 23:13:14.016994','-','-','10:39 AM'), 
('14','V001','2020-07-15 23:13:14.016994','-','-','10:39 AM'), 
('15','V001','2020-07-30 23:13:14.016994','-','-','10:39 AM'), 
('16','V002','2020-07-01 23:13:14.016994','-','-','10:39 AM'), 
('17','V002','2020-07-15 23:13:14.016994','-','-','11:08 AM'), 
('18','V002','2020-07-30 23:13:14.016994','-','-','11:08 AM'), 
('19','V003','2020-07-01 23:13:14.016994','-','-','11:08 AM'), 
('20','V003','2020-07-15 23:13:14.016994','-','-','11:08 AM'), 
('21','V003','2020-07-30 23:13:14.016994','-','-','11:08 AM'), 
('22','V004','2020-07-01 23:13:14.016994','-','-','02:07 PM'), 
('23','V004','2020-07-15 23:13:14.016994','-','-','02:07 PM'), 
('24','V004','2020-07-30 23:13:14.016994','-','-','02:07 PM'); 

INSERT INTO "Zones"("Name","Description") 
VALUES 
('Zone1','First20'), 
('Zone2','Second20'), 
('Zone3','Third20'), 
('Zone4','Fourth20'), 
('Zone5','Fifth20'), 
('Zone6','Sixth20'), 
('Zone7','Seventh20'); 

INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (1, 'V001', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (2, 'V002', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (3, 'V003', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (4, 'V004', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (5, 'V005', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (6, 'V006', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (7, 'V007', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (8, 'V008', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (9, 'V009', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (10, 'V010', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (11, 'V011', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (12, 'V012', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (13, 'V013', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (14, 'V014', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (15, 'V015', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (16, 'V016', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (17, 'V017', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (18, 'V018', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (19, 'V019', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (20, 'V020', 1, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (21, 'V021', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (22, 'V022', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (23, 'V023', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (24, 'V024', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (25, 'V025', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (26, 'V026', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (27, 'V027', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (28, 'V028', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (29, 'V029', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (30, 'V030', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (31, 'V031', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (32, 'V032', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (33, 'V033', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (34, 'V034', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (35, 'V035', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (36, 'V036', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (37, 'V037', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (38, 'V038', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (39, 'V039', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (40, 'V040', 2, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (41, 'V041', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (42, 'V042', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (43, 'V043', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (44, 'V044', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (45, 'V045', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (46, 'V046', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (47, 'V047', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (48, 'V048', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (49, 'V049', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (50, 'V050', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (51, 'V051', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (52, 'V052', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (53, 'V053', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (54, 'V054', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (55, 'V055', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (56, 'V056', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (57, 'V057', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (58, 'V058', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (59, 'V059', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (60, 'V060', 3, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (61, 'V061', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (62, 'V062', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (63, 'V063', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (64, 'V064', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (65, 'V065', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (66, 'V066', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (67, 'V067', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (68, 'V068', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (69, 'V069', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (70, 'V070', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (71, 'V071', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (72, 'V072', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (73, 'V073', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (74, 'V074', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (75, 'V075', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (76, 'V076', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (77, 'V077', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (78, 'V078', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (79, 'V079', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (80, 'V080', 4, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (81, 'V081', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (82, 'V082', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (83, 'V083', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (84, 'V084', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (85, 'V085', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (86, 'V086', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (87, 'V087', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (88, 'V088', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (89, 'V089', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (90, 'V090', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (91, 'V091', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (92, 'V092', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (93, 'V093', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (94, 'V094', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (95, 'V095', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (96, 'V096', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (97, 'V097', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (98, 'V098', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (99, 'V099', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (100, 'V100', 5, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (101, 'V101', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (102, 'V102', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (103, 'V103', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (104, 'V104', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (105, 'V105', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (106, 'V106', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (107, 'V107', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (108, 'V108', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (109, 'V109', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (110, 'V110', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (111, 'V111', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (112, 'V112', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (113, 'V113', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (114, 'V114', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (115, 'V115', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (116, 'V116', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (117, 'V117', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (118, 'V118', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (119, 'V119', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (120, 'V120', 6, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (121, 'V121', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (122, 'V122', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (123, 'V123', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (124, 'V124', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (125, 'V125', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (126, 'V126', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (127, 'V127', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (128, 'V128', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (129, 'V129', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (130, 'V130', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (131, 'V131', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (132, 'V132', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (133, 'V133', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (134, 'V134', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (135, 'V135', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (136, 'V136', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (137, 'V137', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (138, 'V138', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (139, 'V139', 7, false);
INSERT INTO public."Boards" ("ID", "Name", "ZoneID", "IsAtSite") VALUES (140, 'V140', 7, false);

 
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (1, 'J.Pahang', 101.6988056, 3.171541667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (2, 'J.Pahang/Genting Klang', 101.7009111, 3.180175);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (3, 'J.Pahang/Genting Klang', 101.7045111, 3.188636111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (4, 'J.Pahang/Genting Klang', 101.7045667, 3.188547222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (5, 'J.Genting Klang near road entry TAR', 101.7290889, 3.210463889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (6, 'J.Pantai Baru/J.Bangsar/J.Travers/J.Damansara', 101.6874333, 3.137041667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (7, 'J.Jelatek', 101.7349194, 3.163977778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (8, 'J.Ampang/J.Gereja', 101.7070583, 3.157697222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (9, 'J.Ampang/J.Gereja', 101.7156583, 3.160133333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (10, 'J.Ampang/J.Gereja', 101.7238611, 3.1603);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (11, 'J.Ampang/J.Gereja', 101.7308528, 3.160677778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (12, 'J.Ampang/J.Gereja', 101.7430361, 3.159975);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (13, 'J.Ampang/J.Gereja', 101.7429278, 3.1599);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (14, 'J.Ampang/J.Gereja', 101.7529889, 3.155672222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (15, 'J.Raja Chulan', 101.7087889, 3.1504);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (16, 'J.Raja Chulan', 101.7139139, 3.149741667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (17, 'J.Sultan Ismail', 101.7002056, 3.159036111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (18, 'J.Sultan Ismail', 101.700225, 3.159144444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (19, 'J.Sultan Ismail', 101.7103917, 3.150944444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (20, 'Kampung Pandan', 101.7268361, 3.140013889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (21, 'Jalan Pudu', 101.6885722, 3.150305556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (22, 'Jalan Pudu - 3RD party Works', 101.6929583, 3.1595);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (23, 'J.Pudu', 101.7048417, 3.173);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (24, 'J.Cheras/Grand Saga', 101.7152556, 3.165469444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (25, 'J.Cheras/Grand Saga', 101.7301139, 3.108930556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (26, 'J.Cheras/Grand Saga', 101.7333639, 3.103444444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (27, 'J.Cheras/Grand Saga', 101.7417194, 3.085263889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (28, 'J.Cheras/Grand Saga - dismantled', 101.7459528, 3.078741667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (29, 'J.Maharajalela, toWards Jln L1e YeW', 101.6987722, 3.139144444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (30, 'J.Maharajalela, toWards Jln Kinabalu', 101.6987333, 3.139113889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (31, 'Jalan L1e YeW', 101.7075722, 3.132980556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (32, 'Jalan L1e YeW', 101.7075111, 3.132947222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (33, 'Jalan L1e YeW', 101.7154028, 3.12515);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (34, 'Jalan L1e YeW', 101.7230056, 3.118036111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (35, 'Sg.Besi/Besraya', 101.7087806, 3.123113889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (36, 'Sg.Besi/Besraya', 101.7048528, 3.172877778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (37, 'Sg Besi/Besraya nearby NIRVANA', 101.6976778, 3.088730556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (38, 'Sg Besi/Besraya before Kesas IC', 101.7059028, 3.068552778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (39, 'Besraya before Balakong Toll', 101.7068333, 3.0502);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (40, 'Imbi/Hang Tuah/Wisma Putra - dismantled', 101.7153361, 3.146280556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (41, 'Imbi/Hang Tuah/Wisma Putra', 101.6751417, 3.149461111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (42, 'Jalan Istana', 101.6706972, 3.159263889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (43, 'Jalan Istana', 101.6702861, 3.167308333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (44, 'KL-Seremban(before Kuchai Lama interchange towards KL)', 101.6971972, 3.087405556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (45, 'KL-Seremban(towards Sg.Besi toll opposite furniture mall)', 101.7029389, 3.076338889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (46, 'KL-Seremban(before Besraya HighWay interchange towards KL)', 101.7029583, 3.068269444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (47, 'KL-Seremban(before Sg.Besi toll from KL)', 101.7048833, 3.057244444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (48, 'KL-Seremban(after Sg.Besi toll before KESAS interchange)', 101.7047028, 3.057236111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (49, 'KL-Seremban(after Sg.Besi toll towards KL)', 101.7053139, 3.045975);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (50, 'Pintasan Puchong/Sg Besi', 101.691875, 3.164222222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (51, 'Pintasan Puchong/Sg Besi', 101.6860111, 3.169805556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (52, 'Federal HighWay(before Taman Desa Junction towards Cheras Metramac)', 101.6910194, 3.109119444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (53, 'Federal HighWay(before Old Klang Rd towards PJ from Cheras Metramac)', 101.6909694, 3.109077778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (54, 'Federal HighWay(after Cheras Metramac toll towards PJ)', 101.6999944, 3.099566667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (55, 'J.Klang Lama(before exit to FHW)', 101.7088778, 3.123075);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (56, 'J.Syed Putra(towards FHW)', 101.7069722, 3.141613889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (57, 'J.Syed Putra(towards KL)', 101.6951889, 3.137558333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (58, 'J.Syed Putra(towards KL)', 101.7054778, 3.144616667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (59, 'J.Syed Putra(towards PJ)', 101.7095611, 3.140011111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (60, 'J.Syed Putra(before J.Klang Lama toWards FHW)', 101.6815389, 3.174941667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (61, 'J.Syed Putra(from FHW near MidValley)', 101.6952333, 3.137538889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (62, 'Federal HighWay(after arch towards Klang)', 101.6554, 3.111555556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (63, 'Federal HighWay(before arch towards KL)', 101.6552528, 3.111652778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (64, 'Federal HighWay(after KWSP towards Klang)', 101.6507472, 3.108711111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (65, 'Federal HighWay(after J.Utara/J.Timur Junction towards PJ)', 101.6436056, 3.105183333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (66, 'Federal HighWay(before J.Utara/J.Timur junction towards KL)', 101.6436056, 3.105044444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (68, 'NULL', 0, 0);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (69, 'NULL', 0, 0);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (70, 'Federal HighWay(after Motorola LDP Junction toWards KL)', 101.6197389, 3.084677778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (71, 'Federal HighWay', 101.5960111, 3.084055556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (72, 'Federal HighWay', 101.59575, 3.0843);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (73, 'Federal HighWay (after Subang airport junction toWards Klang)', 101.5833306, 3.083666667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (74, 'Federal HighWay (before Subang airport junction toWards KL)', 101.5832333, 3.083808333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (75, 'Federal HighWay (before Jln Jubli Perak after Batu 3 toll toWards Klang)', 101.5589778, 3.075983333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (76, 'Federal HighWay (before Persiaran Kayangan toWards Klang)', 101.5442556, 3.071316667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (80, 'Federal HighWay (before Persiaran Sultan toWards KL)', 101.5114944, 3.063777778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (81, 'Federal HighWay (before Padang JaWa toWards Klang)', 101.496025, 3.062486111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (82, 'Cheng Lock/Tun Sambathan', 101.6898056, 3.135672222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (84, 'J.Pantai Baru/J.Bangsar/J.Travers/J.Damansara', 101.6838056, 3.133752778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (85, 'J.Pantai Baru/J.Bangsar/J.Travers/J.Damansara', 101.6805, 3.129577778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (86, 'J.Pantai Baru/J.Bangsar/J.Travers/J.Damansara', 101.6803806, 3.129127778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (87, 'J.Pantai Baru/J.Bangsar/J.Travers/J.Damansara', 101.6768611, 3.1238);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (88, 'Bukit Jalil', 101.6748361, 3.127966667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (89, 'Jalan Damansara (near Muzium Negara toWards Jln Istana)', 101.6933556, 3.049155556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (91, 'Jalan Damansara (toWards LBH Mahameru)', 101.6909611, 3.137866667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (92, 'J.Damansara', 101.6896139, 3.127922222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (93, 'J.Duta', 101.6842194, 3.124861111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (94, 'J.Duta', 101.6876806, 3.150155556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (95, 'J.Duta', 101.6794333, 3.119288889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (96, 'J.Duta', 101.7023944, 3.126744444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (97, 'J.Duta', 101.6932889, 3.049269444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (98, 'J.Duta', 101.6914611, 3.165091667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (83, 'Jalan Pantai Baru/Jalan Bangsar/Jalan Travers/Jalan Damansara', 101.690961111, 3.137867);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (77, 'Federal HighWay (after Persiaran Kayangan toWards KL)', 101.713356, 3.087389);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (78, 'Federal HighWay (before Persiaran Sultan toWards Klang)', 101.714619, 3.085897);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (99, 'J.Parlimen/Tun Perak', 101.7144556, 3.166563889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (100, 'J.Parlimen/Tun Perak', 101.6692861, 3.197469444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (101, 'Jalan Kuching (before Bank Negara from Jalan Kinabalu)', 101.6924417, 3.147011111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (102, 'Jalan Kuching (before to Jln Sultan Ismail junction toWards Kepong)', 101.7143889, 3.133463889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (103, 'J.Kuching, from Kepong towards J.Parlimen', 101.7057333, 3.122386111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (104, 'J.Kuching, before LBH Mahameru towards Kepong near The Mall', 101.6908389, 3.137255556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (105, 'J.Kuching, before Lbh Mahameru from Kepong near The Mall', 101.6897111, 3.137883333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (106, 'J.Kuching, before The Mall towards KL', 101.6799222, 3.141375);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (107, 'J.Kuching, before bulatan Segambut towards Kepong', 101.6717694, 3.154980556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (108, 'J.Kuching,before bulatan Segambut from Kepong', 101.6710806, 3.16865);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (109, 'J.Kuching, before Bulatan Kepong from KL', 101.6365722, 3.100705556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (110, 'J.Kepong Opposite Petronas', 101.6663083, 3.206422222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (111, 'J.Kepong toWard KL', 101.6474667, 3.211652778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (113, 'J.Raja Laut befor Tan Chong ShoWroom', 101.6964222, 3.167866667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (114, 'J.Ipoh', 101.6889472, 3.175552778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (115, 'J.Ipoh', 101.6888528, 3.175794444);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (116, 'J.Ipoh', 101.677975, 3.199372222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (117, 'J.Ipoh nearby Taman Wah Yew', 101.6780278, 3.199363889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (118, 'J.Ipoh', 101.6718167, 3.212133333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (119, 'J.Ipoh', 101.6717667, 3.221711111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (120, 'J.Sentul', 101.6919056, 3.184277778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (121, 'J.Mahameru', 101.6781222, 3.104952778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (122, 'J.Mahameru', 101.6946, 3.155222222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (123, 'J.Tun Razak', 101.6852528, 3.163361111);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (124, 'J.Tun Razak', 101.6807667, 3.154947222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (125, 'J.Tun Razak', 101.6932417, 3.159247222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (126, 'J.Tun Razak', 101.72125, 3.128477778);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (127, 'J.Tun Razak', 101.7222722, 3.154241667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (128, 'J.Tun Razak', 101.7227083, 3.153508333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (129, 'MRR2', 101.6606, 3.231605556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (130, 'J.Genting Klang', 101.7356056, 3.219513889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (131, 'MRR2 (before Wangsa Maju junction)', 101.7488222, 3.208633333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (132, 'MRR2 (after Jln Ampang toWards Keramat)', 101.7547306, 3.171083333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (133, 'MRR2 (before Jln Ampang from Wangsa Maju)', 101.7537083, 3.168883333);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (134, 'MRR2 (after Pandan Indah toWards Ampang)', 101.7455083, 3.151772222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (135, 'MRR2 (next to furniture shop Cempaka)', 101.7432111, 3.141697222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (136, 'MRR2 (before Pandan Indah toWards Ampang)', 101.7466917, 3.1268);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (137, 'MRR2 (opposite Safari)', 101.7444278, 3.110547222);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (138, 'MRR2 (before MAKRO toWards Pandan)', 101.7349, 3.097388889);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (139, 'MRR2 (before Bandar Tasik Selatan toWards KESAS)', 101.7195667, 3.079416667);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (140, 'MRR2 (next to Desa Tun Razak Flat)', 101.719475, 3.079525);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (67, 'Salak HighWay towards Mid Valley', 101.63666667, 3.10055556);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (79, 'Federal HighWay (before Persiaran Kayangan toWards KL)', 101.729647, 3.081922);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (90, 'Jalan Damansara (near Muzium Negara )', 101.688967, 3.138369);
INSERT INTO public."Locations" ("BoardID", "Address", "Longitude", "Latitude") VALUES (112, 'J.Raja Laut', 101.694722, 3.154894);

INSERT INTO "EditorType"("Type") 
VALUES 
('Parking'), 
('Upload'), 
('Traveltime'),
('Custom'),
('Weather');

INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (1, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, 'OFFLINE/SLEEP', 'https://homepages.cae.wisc.edu/~ece533/images/cat.png', '2020-08-24 12:52:06.564503', 'THIS IS A CAT!', false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (2, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (3, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (83, '121.120.145.186', '', '2020-06-11 00:00:00', 1, false, true, 'http://121.120.145.186/api/screenshot', NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (78, '10.20.3.201', '', '2020-07-07 00:00:00', 1, false, true, 'http://10.20.3.201/api/screenshot', NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (90, '10.20.0.200', '', '2020-06-11 00:00:00', 1, false, true, 'http://10.20.0.200/api/screenshot', NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (112, '10.20.6.61', '', '2020-06-11 00:00:00', 1, false, true, 'http://10.20.6.61/api/screenshot', NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (77, '10.20.3.200', 'rtsp://admin:ITIS12345@10.20.5.23:554/Streaming/Channels/1', '2020-06-11 00:00:00', 1, false, true, 'http://10.20.3.200/api/screenshot', NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (48, '10.20.8.10', '', '2020-08-11 00:00:00', 1, false, true, 'http://10.20.8.10/api/screenshot', NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (73, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (74, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (75, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (76, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (79, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (80, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (81, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (82, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (84, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (85, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (86, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (87, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (88, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (89, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (91, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (4, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (5, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (6, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (7, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (8, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (9, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (10, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (11, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (12, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (13, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (14, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (15, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (16, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (17, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (18, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (19, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (20, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (21, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (22, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (23, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (24, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (25, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (26, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (27, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (28, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (29, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (30, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (92, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (31, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (32, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (33, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (34, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (35, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (36, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (37, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (38, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (39, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (40, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (41, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (42, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (43, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (44, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (45, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (46, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (47, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (49, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (50, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (51, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (52, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (53, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (54, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (55, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (56, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (57, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (58, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (59, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (60, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (61, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (62, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (63, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (64, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (65, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (66, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (67, 'sURL', NULL, '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (68, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (69, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (70, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (71, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (72, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (93, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (94, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (95, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (96, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (97, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (98, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (99, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (100, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (101, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (102, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (103, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (104, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (105, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (106, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (107, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (108, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (109, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (110, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (111, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (113, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (114, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (115, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (116, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (117, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (118, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (119, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (120, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (121, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (122, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (123, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (124, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (125, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (126, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (127, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (128, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (129, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (130, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (131, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (132, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (133, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (134, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (135, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (136, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (137, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (138, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (139, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
INSERT INTO public."Displays" ("BoardID", "C4_IP", "CCTV", "InstallationDate", "OperationalStatus", "HasAlert", "IsOnline", "Screenshot_URL", "AlibabaAccidentImage", "AlibabaCreationDate", "AlibabaText", "HasUniqueDisplay") VALUES (140, 'sURL', '', '2020-08-11 00:00:00', 3, false, false, NULL, NULL, '0001-01-01 00:00:00', NULL, false);
 
INSERT INTO "TrafficInfos" ("InfoProviderID","Board","Event","TravelTime","PointA","PointB","NowDateTime") 
VALUES 
('1','1','1','20','VZ001','TIMES SQUARE','2001-09-29 03:00'),
('2','1','1','12','VZ001','MID VALLEY','2001-09-29 03:00');      

INSERT INTO "TravelTimeInfos_B1s"("id","eventType","sname","name","eta","datetime")
VALUES
('1','1','M1','Dest1','111','datetimetext'),
('2','1','M2','Dest2','222','datetimetext'),
('3','1','M3','Dest3','333','datetimetext'),
('4','1','M4','Dest4','444','datetimetext');

INSERT INTO "Videos"("MessageID","VidType","BoardID","Bname","Message","NowDateTime")
VALUES
('1','Celebration','1','VZ001','video1.mp4','2001-09-29 03:00'),
('2','Emergency','2','VZ001','','2001-09-29 03:00');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (168, 'SELAYANG', 'No rain', 'No rain', 'No rain', '2020-09-15');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (167, 'KEPONG', 'No rain', 'No rain', 'No rain', '2020-09-15');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (162, 'AMPANG', 'No rain', 'No rain', 'No rain', '2020-09-15');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (164, 'BUKIT BINTANG', 'No rain', 'No rain', 'No rain', '2020-09-15');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (340, 'KUALA LUMPUR', 'No rain', 'No rain', 'No rain', '2020-09-15');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (165, 'CHERAS', 'No rain', 'No rain', 'No rain', '2020-09-15');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (166, 'JALAN DUTA', 'No rain', 'No rain', 'No rain', '2020-09-15');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (163, 'BANGSAR', 'No rain', 'No rain', 'No rain', '2020-09-15');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (170, 'SUNGAI BESI', 'No rain', 'No rain', 'No rain', '2020-09-15');
INSERT INTO public."WeatherForecast" ("ID", "Location", "MorningCondition", "AfternoonCondition", "NightCondition", "Date") VALUES (169, 'SETAPAK', 'No rain', 'No rain', 'No rain', '2020-09-15');


