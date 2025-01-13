# יצירת פרוייקט חדש
פתחו את תיקיית הפרוייקטים שלכם והריצו את הפקודה הבאה (וודאו להחליף את [your project name] בשם הפרוייקט שלכם):
```
git clone https://github.com/yuvalvolinski/ChatProject.git [your project name]
```

# הרצת הפרוייקט

על מנת להריץ את השרת מבלי להצטרך לפתוח את סביבת הפיתוח כמנהל, ניתן לאפשר גישה לפורט רצוי (במקרה שלנו, פורט 5000) בעזרת פתיחת שורת המשימות כמנהל והרצת הפקודה:

```
netsh http add urlacl url=http://*:5000/ user=Everyone
```
לאחר מכן נפתח את סביבת הפיתוח ונריץ את הפרוייקט בעזרת הפקודה:
```
dotnet run
יש לפתוח  אתר  בככתובת:
http://localhost:5000/website/pages/login.html
יש  להגדיר את המיקום הפרוייקט , לדוגמה:
 string basePath  = @"D:\work\Yuval\Project 1 - Chat\Chat_project";
אם הגדרה הזאת ריקה, יש להעתיק ספריה:website  ל\bin\Debug\net8.0
אפשר לראות בConsoole את מיקום הרצת אפחקציה
