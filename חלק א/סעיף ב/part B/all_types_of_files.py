import pandas as pd
import os


def load_data(file_path):
    file_extension = os.path.splitext(file_path)[1]

    if file_extension == ".csv":
        df = pd.read_csv(file_path, parse_dates=["timestamp"])
    elif file_extension == ".parquet":
        df = pd.read_parquet(file_path)
    else:
        raise ValueError("Unsupported file format! Please use CSV or Parquet.")

    return df


file_path = "time_series (4).parquet"
df = load_data(file_path)
print(df)

df["hour"] = df["timestamp"].dt.floor('h')
hourly_avg = df.groupby("hour")["mean_value"].mean().reset_index()

hourly_avg.to_csv("hourly_avg_output.csv", index=False)

print(hourly_avg)
print("הקובץ נשמר בהצלחה!")
