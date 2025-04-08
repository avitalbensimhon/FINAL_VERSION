import pandas as pd

file_path = "time_series.csv"
df = pd.read_csv(file_path)

df["timestamp"] = pd.to_datetime(df["timestamp"], dayfirst=True, errors="coerce")

df['value'] = pd.to_numeric(df['value'], errors='coerce')

df["date"] = df["timestamp"].dt.date


def calculate_hourly_avg_for_day(day_df):
    day_df["hour"] = day_df["timestamp"].dt.floor('h')

    hourly_avg = day_df.groupby("hour")["value"].mean().reset_index()
    return hourly_avg


daily_avg_results = []

for date, day_df in df.groupby("date"):
    hourly_avg = calculate_hourly_avg_for_day(day_df)
    hourly_avg["date"] = date
    daily_avg_results.append(hourly_avg)

final_result = pd.concat(daily_avg_results)

final_result.columns = ["זמן התחלה", "ממוצע", "תאריך"]

final_result.to_csv("hourly_avg_results.csv", index=False)

print(final_result)
