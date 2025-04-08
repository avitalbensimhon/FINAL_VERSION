import pandas as pd
import numpy as np

file_path = "time_series.csv"


def validate_time_series(file_path):
    try:

        df = pd.read_csv(file_path)

        required_columns = {"timestamp", "value"}
        if not required_columns.issubset(df.columns):
            raise ValueError(f"חסרות עמודות נדרשות: {required_columns - set(df.columns)}")

        df["timestamp"] = pd.to_datetime(df["timestamp"], dayfirst=True, errors="coerce")

        missing_values = df.isnull().sum()
        if missing_values.any():
            print(f"⚠️ יש ערכים חסרים:\n{missing_values.to_string(index=False)}")

        if df["timestamp"].isna().any():
            print("⚠️ נמצאו חותמות זמן לא תקינות!")

        df["value"] = pd.to_numeric(df["value"], errors="coerce")
        if df["value"].isna().any():
            print("⚠️ נמצאו ערכים לא מספריים בעמודת 'value'!")

        duplicate_rows = df.duplicated().sum()
        if duplicate_rows > 0:
            print(f"⚠️ נמצאו {duplicate_rows} שורות כפולות!")

        duplicate_timestamps = df.duplicated(subset=["timestamp"], keep=False)
        if df[duplicate_timestamps].shape[0] > 0:
            print("⚠️ נמצאו חותמות זמן כפולות עם ערכים שונים!")

        if not df["timestamp"].is_monotonic_increasing:
            print("⚠️ הנתונים אינם ממוינים לפי סדר הזמן!")
        df["hour"] = df["timestamp"].dt.floor('h')
        hourly_avg = df.groupby("hour")["value"].mean().reset_index()
        hourly_avg.columns = ["זמן התחלה", "ממוצע"]

        print(hourly_avg)

        print("✅ הנתונים עברו בדיקות בסיסיות!")
        return df

    except Exception as e:
        print(f"❌ שגיאה: {e}")
        return None


df_clean = validate_time_series(file_path)
