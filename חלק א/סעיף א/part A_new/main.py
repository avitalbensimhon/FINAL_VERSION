import errors


def press_num_of_errors():
    n = int(input("press num of errors: "))
    errors.top_n_errors(n)


press_num_of_errors()