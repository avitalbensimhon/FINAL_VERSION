import pandas as pd


df = pd.read_excel("logs.txt.xlsx", engine="openpyxl")


count_array = []


def part(i, df):

    part_from_files = df.iloc[i:i + 1000].copy()
    part_from_files.columns = ['Log']
    part_from_files[['Timestamp', 'Error Code']] = part_from_files['Log'].str.split(',', expand=True)


    part_from_files.loc[:, 'Error Code'] = part_from_files['Error Code'].str.replace('Error: ', '').str.strip()
    part_from_files.loc[:, 'Timestamp'] = part_from_files['Timestamp'].str.replace('Timestamp: ', '').str.strip()


    error_counts = part_from_files['Error Code'].value_counts()


    count_array.append(error_counts)


def top_n_errors(n):
    i = 0
    total_rows = len(df)


    while i < total_rows:
        part(i, df)
        print(f"Processed {i + 1000} rows")
        i += 1000


    total_error_counts = pd.concat(count_array).groupby(level=0).sum()


    top_n_error = total_error_counts.head(n)
    print(top_n_error.reset_index().to_string(index=False))

#ניתוח סיבוכיות זמן
#1. df = pd.read_excel(...)
#סיבוכיות זמן: O(n) – קריאת כל הנתונים מקובץ Excel, כאשר n הוא מספר השורות בקובץ.
#2. הפונקציה part(i, df)

#פועלת על 1000 שורות בכל פעם → קבוע (נניח k=1000)

#פעולות כמו:

#iloc[i:i+1000] – O(k)

#str.split – O(k)

#value_counts() – O(k)

#סה"כ: O(k) (נחשב קבוע)

#3. הלולאה ב־top_n_errors(n)

#מספר איטרציות: n / 1000 = O(n)

#בכל איטרציה → פעולה של part() שהיא O(1)


#סה"כ: O(n)

#4. pd.concat(count_array).groupby(...).sum()

#מאחד את התוצאות מכל הקטעים

#סיבוכיות: O(n) – כי מדובר באיחוד וסיכום של כל השורות

#5. head(n) ו־reset_index() על התוצאה
#סיבוכיות: O(n) – אם לא ממוין, אחרת O(n log n) אם יש מיון לפני

#סיכום סיבוכיות זמן:
#O(n) – ליניארי ביחס לגודל הקובץ.

#ניתוח סיבוכיות מקום

#1. df – הקובץ כולו בזיכרון
#גודל: O(n)

#2. count_array – אוסף של value_counts() מכל קטע

#כל קטע שומר מפתחות (Error Code) וכמותם

#יש n / 1000 קטעים → כל אחד שומר O(m) רשומות (אם m הוא מספר שגיאות שונות)

#סה"כ: O(m * n/1000) ≈ O(n) במקרה הגרוע

#3. תוצאה סופית total_error_counts
#גם כן O(m)

# סיכום סיבוכיות מקום:
#O(n) – זיכרון ליניארי ביחס לגודל הקובץ.