use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day01::solve_part_one(input),
        Part::P2 => Day01::solve_part_two(input),
    }
}

struct Day01;
impl Solution for Day01 {
    fn solve_part_one(input: String) -> String {
        let values: Vec<i32> = input
            .lines()
            .map(|line| {
                let digits: Vec<i32> = line
                    .chars()
                    .filter_map(|c| c.to_digit(10))
                    .map(|d| d as i32)
                    .collect();
                format!("{}{}", digits.first().unwrap(), digits.last().unwrap())
                    .parse()
                    .unwrap()
            })
            .collect();
        values.iter().sum::<i32>().to_string()
    }

    fn solve_part_two(input: String) -> String {
        let values: Vec<i32> = input
            .lines()
            .map(|line| {
                let translated = translate_text_numbers(line);
                let digits: Vec<i32> = translated
                    .chars()
                    .filter_map(|c| c.to_digit(10))
                    .map(|d| d as i32)
                    .collect();
                format!("{}{}", digits.first().unwrap(), digits.last().unwrap())
                    .parse()
                    .unwrap()
            })
            .collect();
        values.iter().sum::<i32>().to_string()
    }
}

fn translate_text_numbers(text: &str) -> String {
    text.replace("one", "o1e")
        .replace("two", "t2o")
        .replace("three", "t3e")
        .replace("four", "f4r")
        .replace("five", "f5e")
        .replace("six", "s6x")
        .replace("seven", "s7n")
        .replace("eight", "e8t")
        .replace("nine", "n9e")
        .replace("zero", "z0o")
}
