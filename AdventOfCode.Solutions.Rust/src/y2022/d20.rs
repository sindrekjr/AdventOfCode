use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day20::solve_part_one(input),
        Part::P2 => Day20::solve_part_two(input),
    }
}

#[derive(Clone, Copy, Debug, Eq, PartialEq)]
struct Number {
    s_i: usize,
    value: i64,
}

struct Day20;
impl Solution for Day20 {
    fn solve_part_one(input: String) -> String {
        let mut numbers: Vec<Number> = input
            .lines()
            .enumerate()
            .map(|(i, line)| Number {
                s_i: i,
                value: line.parse::<i64>().unwrap(),
            })
            .collect();
        let count = numbers.len() as i64 - 1;

        for i in 0..numbers.len() {
            let a_i = numbers.iter().position(|n| n.s_i == i).unwrap();
            let number = numbers.remove(a_i);
            let n_i = (((a_i as i64 + number.value) % count) + count) % count;
            numbers.insert(n_i as usize, number);
        }

        let z_i = numbers.iter().position(|n| n.value == 0).unwrap();

        (numbers[(z_i + 1000) % numbers.len()].value
            + numbers[(z_i + 2000) % numbers.len()].value
            + numbers[(z_i + 3000) % numbers.len()].value)
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        const DECRYPTION_KEY: i64 = 811589153;

        let mut numbers: Vec<Number> = input
            .lines()
            .enumerate()
            .map(|(i, line)| Number {
                s_i: i,
                value: line.parse::<i64>().unwrap() * DECRYPTION_KEY,
            })
            .collect();
        let count = numbers.len() as i64 - 1;

        for _ in 0..10 {
            for i in 0..numbers.len() {
                let a_i = numbers.iter().position(|n| n.s_i == i).unwrap();
                let number = numbers.remove(a_i);
                let n_i = (((a_i as i64 + number.value) % count) + count) % count;
                numbers.insert(n_i as usize, number);
            }
        }

        let z_i = numbers.iter().position(|n| n.value == 0).unwrap();

        (numbers[(z_i + 1000) % numbers.len()].value
            + numbers[(z_i + 2000) % numbers.len()].value
            + numbers[(z_i + 3000) % numbers.len()].value)
            .to_string()
    }
}
