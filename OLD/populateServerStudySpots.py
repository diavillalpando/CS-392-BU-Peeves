import pymongo
import os

myclient = pymongo.MongoClient("mongodb://localhost:27017/")
mydb = myclient["LoginDb"]
myCol = mydb["StudySpots"]

def addSpot(name, address, description, imageUrl, lat, lng):
   newSpot = {
      "Name": name,
      "Address": address,
      "Description": description,
      "Image": imageUrl,
      "Lat": lat, 
      "Lng": lng,
   }
   x = myCol.insert_one(newSpot)
   print("Added '{}' ({}) to db".format(name,x.inserted_id))

os.system("clear")
print("---Populating StudySpot Db---")

addSpot("Mugar Memorial Library","771 Commonwealth Ave","Mugar is a second home for many BU students during finals. It will stay open 24 hours from Wednesday, December 13, through Friday, December 22, at 11 pm. The library has seven floors and the largest computer lounge on campus—and the floors get quieter the higher you go. The ground floor makes for an ideal group study environment.", "https://www.bu.edu/files/2018/12/mugar-15-9446-FINALS-052.jpg",42.35113856448191, -71.10804522886141)
addSpot("Educational Resource Center","100 Bay State Road","At the Center for Student Services on the fifth floor: The Educational Resource Center (ERC) has open-concept study areas and private rooms available on a first-come, first-served basis. Visit the fifth-floor office to reserve a private space. The ERC is open to all students with a valid BU ID.", "https://www.bu.edu/files/2018/12/educational-resource-center-StudySpots-6.jpg",42.34990316270545, -71.09794197404592)
addSpot("Pickering Educational Resources Library","2 Silber Way","The Pickering Library, in the college’s basement, is one of the smallest libraries on campus and offers quiet rooms for independent study, as well as group work rooms and a study lounge. Open to all students with a valid BU ID", "https://www.bu.edu/files/2018/12/pickering-library-18-1892-STUDY-007.jpg",42.34982353678412, -71.10084107375718)
addSpot("Science & Engineering Library","38 Cummington Mall","This library is convenient for anyone living on or near East Campus seeking a short commute. It has cubicles as well as communal tables for studying.", "https://www.bu.edu/files/2018/12/science-and-engineering-library-StudySpots-9.jpg",42.348816077966426, -71.10181729243371)
addSpot("HoJo","575 Commonwealth Ave","Eight Floor: This top-floor study lounge is for those who want a view while hitting the books. The hotel-turned-residence hall also has a first-floor multipurpose room with chairs, couches, and a pool table for when you need a break. The study lounge is open only to students living on campus, and after 2 am is available only to HoJo residents.", "https://www.bu.edu/housing/files/2019/11/19-1780-HOUSING-029-1920x624.jpg",42.349806387909204, -71.0986882805034)
addSpot("Center for Computing & Data Sciences","665 Commonwealth Ave","The 19-story Center for Computing & Data Sciences is open for learning, lounging, and eating, and students can find many nooks to do some studying, too. The bottom two floors are your best bet for casual solo or group work (and be sure to grab a snack at the student-run cafe, Saxbys). Or hunker down in a quiet study corner with whiteboards on many of the upper floors.", "https://www.bu.edu/files/2023/01/CCDS-First-Day-23-1022-CCDSOPEN-006-resize.jpg",42.350087368934524, -71.10320864492135)
addSpot("College of Arts & Sciences Think Tank","725 Commonwealth Ave","Room 105: The Think Tank features individual study cubbies, communal tables, and team meeting rooms with whiteboards. The space can accommodate up to 134 students and is open to all BU students.", "https://www.bu.edu/files/2018/12/cas-thinktank-17-1355-RIBBON-010.jpg",42.35045323582433, -71.10539810103047)
addSpot("Stone Science Library","725 Commonwealth Ave","Room 440: For a central and quiet stop between exams, students can tuck away in the Stone Science Library, a non-circulating research library focusing on archaeological and remote-sensing materials. Found on the fourth floor of the College of Arts & Sciences, the spot boasts large windows and plenty of plants.", "https://www.bu.edu/files/2022/05/12-4617-STONELIB-020.jpeg",42.350269381188696, -71.10374098724962)
addSpot("School of Theology Library","745 Commonwealth Ave., second floor","second floor: The STH Library has carrels as well as communal tables for studying. There is also a conference room that can be booked for groups of students needing a place to gather. Reserve a group study room here. The library is open to all students with a valid BU ID.", "https://www.bu.edu/files/2018/12/sth-library-StudySpots2-2.jpg",42.35053706436893, -71.10719737190615)
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")

# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")
# addSpot("name","address","Description", "ImageUrl")


