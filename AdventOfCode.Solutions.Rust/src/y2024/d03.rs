use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day03::solve_part_one(input),
        Part::P2 => Day03::solve_part_two(input),
    }
}

struct Day03;
impl Solution for Day03 {
    fn solve_part_one(input: String) -> String {
        regex::Regex::new(r"mul\((\d+),(\d+)\)")
            .unwrap()
            .captures_iter(&input)
            .fold(0, |sum, cap| {
                sum + cap[1].parse::<u32>().unwrap() * cap[2].parse::<u32>().unwrap()
            })
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        regex::Regex::new(r"do\(\)|mul\((\d+),(\d+)\)|don't\(\)")
            .unwrap()
            .captures_iter(&input)
            .fold((0, true), |(sum, enabled), cap| {
                if let (Some(a), Some(b)) = (cap.get(1), cap.get(2)) {
                    if enabled {
                        (
                            sum + a.as_str().parse::<u32>().unwrap()
                                * b.as_str().parse::<u32>().unwrap(),
                            enabled,
                        )
                    } else {
                        (sum, enabled)
                    }
                } else {
                    (sum, cap[0].to_string() == "do()")
                }
            })
            .0
            .to_string()
    }
}
