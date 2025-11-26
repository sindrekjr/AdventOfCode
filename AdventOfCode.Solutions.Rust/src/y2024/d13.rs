use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day13::solve_part_one(input),
        Part::P2 => Day13::solve_part_two(input),
    }
}

struct ClawMachine {
    a: (i64, i64),
    b: (i64, i64),
    prize: (i64, i64),
}

impl ClawMachine {
    fn from(str: &str) -> Self {
        let parts: Vec<_> = str
            .lines()
            .map(|line| {
                let (_, info) = line.split_once(": ").unwrap();
                let (x, y) = info.split_once(", ").unwrap();
                (Self::parse_num(x), Self::parse_num(y))
            })
            .collect();

        let a = parts[0];
        let b = parts[1];
        let prize = parts[2];

        Self { a, b, prize }
    }

    fn parse_num(str: &str) -> i64 {
        if let Some(num) = str.split_once('+') {
            num.1.parse().unwrap()
        } else {
            str.split_once('=').unwrap().1.parse().unwrap()
        }
    }

    fn with_conversion(&self, conversion: i64) -> Self {
        Self {
            a: self.a,
            b: self.b,
            prize: (self.prize.0 + conversion, self.prize.1 + conversion),
        }
    }

    fn is_winnable(&self) -> bool {
        self.denominator() != 0
            && self.a_steps() % self.denominator() == 0
            && self.b_steps() % self.denominator() == 0
    }

    fn denominator(&self) -> i64 {
        self.a.0 * self.b.1 - self.a.1 * self.b.0
    }

    fn a_steps(&self) -> i64 {
        self.a.0 * self.prize.1 - self.a.1 * self.prize.0
    }

    fn b_steps(&self) -> i64 {
        self.b.1 * self.prize.0 - self.b.0 * self.prize.1
    }

    fn cost(&self) -> i64 {
        (self.a_steps() / self.denominator()) + 3 * (self.b_steps() / self.denominator())
    }
}

struct Day13;
impl Solution for Day13 {
    fn solve_part_one(input: String) -> Option<String> {
        Some(
            input
                .split("\n\n")
                .filter_map(|paragraph| {
                    let machine = ClawMachine::from(paragraph);
                    if machine.is_winnable() {
                        Some(machine.cost())
                    } else {
                        None
                    }
                })
                .sum::<i64>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        Some(
            input
                .split("\n\n")
                .filter_map(|paragraph| {
                    let machine = ClawMachine::from(paragraph).with_conversion(10000000000000);
                    if machine.is_winnable() {
                        Some(machine.cost())
                    } else {
                        None
                    }
                })
                .sum::<i64>()
                .to_string(),
        )
    }
}
