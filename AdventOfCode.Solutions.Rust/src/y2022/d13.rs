use std::cmp::Ordering;

use crate::{
    core::{Part, Solution},
    utils::string::StrUtils,
};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day13::solve_part_one(input),
        Part::P2 => Day13::solve_part_two(input),
    }
}

enum Value<T> {
    Actual(T),
    List(Vec<Value<T>>),
}

impl Value<i32> {
    fn new(input: &str) -> Self {
        if let Ok(int) = input.parse::<i32>() {
            Self::Actual(int)
        } else {
            let mut depth = 0;
            let mut start = 1;

            let mut values: Vec<Self> = input
                .char_indices()
                .skip(1)
                .filter_map(|(i, ch)| {
                    match ch {
                        '[' => depth += 1,
                        ']' => depth -= 1,
                        ',' if depth == 0 => {
                            let value = Value::new(&input[start..i]);
                            start = i + 1;
                            return Some(value);
                        }
                        _ => (),
                    };

                    None
                })
                .collect();

            if input.len() > 2 {
                values.push(Value::new(&input[start..input.len() - 1]));
            }

            Self::List(values)
        }
    }

    fn cmp(&self, other: &Value<i32>) -> Ordering {
        match (self, other) {
            (Value::List(left), Value::List(right)) => {
                if let Some(order) = left.iter().zip(right.iter()).find_map(|(left, right)| {
                    let order = left.cmp(right);
                    if order != Ordering::Equal {
                        Some(order)
                    } else {
                        None
                    }
                }) {
                    order
                } else {
                    left.len().cmp(&right.len())
                }
            }

            (Value::List(_), Value::Actual(right)) => {
                self.cmp(&Value::List(vec![Value::Actual(*right)]))
            }
            (Value::Actual(left), Value::List(_)) => {
                Value::List(vec![Value::Actual(*left)]).cmp(other)
            }

            (Value::Actual(left), Value::Actual(right)) => left.cmp(&right),
        }
    }
}

struct Pair {
    left: Value<i32>,
    right: Value<i32>,
}

impl Pair {
    fn new(input: &str) -> Self {
        let (left, right) = input.split_once('\n').unwrap();

        Self {
            left: Value::new(left),
            right: Value::new(right),
        }
    }

    fn ordered(&self) -> bool {
        self.left.cmp(&self.right) == Ordering::Less
    }
}

struct Day13;
impl Solution for Day13 {
    fn solve_part_one(input: String) -> String {
        input
            .paragraphs()
            .enumerate()
            .filter_map(|(i, pair)| {
                if Pair::new(pair).ordered() {
                    Some(i + 1)
                } else {
                    None
                }
            })
            .sum::<usize>()
            .to_string()
    }

    fn solve_part_two(_input: String) -> String {
        String::new()
    }
}
