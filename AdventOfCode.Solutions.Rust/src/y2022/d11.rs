use crate::{
    core::{Part, Solution},
    utils::string::StrUtils,
};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day11::solve_part_one(input),
        Part::P2 => Day11::solve_part_two(input),
    }
}

#[derive(Debug)]
enum Operation {
    Add(u64),
    AddSelf,
    Multiply(u64),
    MultiplySelf,
}

#[derive(Debug)]
struct Monkey {
    inspections: u64,
    items: Vec<u64>,
    op: Operation,
    divisible_by: u64,
    t: usize,
    f: usize,
}

impl Monkey {
    fn parse_operation(input: &str) -> Operation {
        let (_, pieces) = input.split_once("old").unwrap();
        match pieces.trim().split_once(' ').unwrap() {
            ("+", "old") => Operation::AddSelf,
            ("+", val) => Operation::Add(val.parse::<u64>().unwrap()),
            ("*", "old") => Operation::MultiplySelf,
            ("*", val) => Operation::Multiply(val.parse::<u64>().unwrap()),
            _ => panic!(),
        }
    }

    // fn parse_decision(input: Vec<&str>) -> impl Fn(u64) -> u64 {
    //     let values: Vec<u64> = input.into_iter().map(|v| v.split_whitespace().last().unwrap().parse::<u64>().unwrap()).collect();

    //     move |item| {
    //         if item % values[0] == 0 {
    //             values[1]
    //         } else {
    //             values[2]
    //         }
    //     }
    // }
}

struct Day11;
impl Solution for Day11 {
    fn solve_part_one(input: String) -> Option<String> {
        let mut monkeys: Vec<Monkey> = input
            .paragraphs()
            .map(|paragraph| {
                let mut monkey = paragraph.lines().skip(1).map(|line| {
                    let (_, val) = line.split_once(':').unwrap();
                    val
                });

                let items = monkey
                    .next()
                    .unwrap()
                    .split(',')
                    .map(|val| val.trim().parse::<u64>().unwrap())
                    .collect();

                let op = Monkey::parse_operation(monkey.next().unwrap());

                let divisible_by = monkey
                    .next()
                    .unwrap()
                    .split(' ')
                    .last()
                    .unwrap()
                    .parse::<u64>()
                    .unwrap();

                let mut decision_values = monkey.into_iter().map(|v| {
                    v.split_whitespace()
                        .last()
                        .unwrap()
                        .parse::<usize>()
                        .unwrap()
                });

                let t = decision_values.next().unwrap();
                let f = decision_values.next().unwrap();

                Monkey {
                    inspections: 0,
                    items,
                    op,
                    divisible_by,
                    t,
                    f,
                }
            })
            .collect();

        for _ in 0..20 {
            for i in 0..monkeys.len() {
                let monkey = &mut monkeys[i];

                let mut items: Vec<(usize, u64)> = vec![];
                while let Some(item) = monkey.items.pop() {
                    monkey.inspections += 1;
                    let inspected_item = match monkey.op {
                        Operation::Add(x) => item + x,
                        Operation::AddSelf => item + item,
                        Operation::Multiply(x) => item * x,
                        Operation::MultiplySelf => item * item,
                    };

                    let bored_item = inspected_item / 3;

                    if bored_item % monkey.divisible_by == 0 {
                        items.push((monkey.t, bored_item));
                    } else {
                        items.push((monkey.f, bored_item));
                    }
                }

                for (receiver, item) in items {
                    monkeys[receiver].items.push(item);
                }
            }
        }

        let mut inspections = monkeys
            .into_iter()
            .map(|monkey| monkey.inspections)
            .collect::<Vec<u64>>();
        inspections.sort();

        Some(
            inspections
                .iter()
                .rev()
                .take(2)
                .product::<u64>()
                .to_string(),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let mut monkeys: Vec<Monkey> = input
            .paragraphs()
            .map(|paragraph| {
                let mut monkey = paragraph.lines().skip(1).map(|line| {
                    let (_, val) = line.split_once(':').unwrap();
                    val
                });

                let items = monkey
                    .next()
                    .unwrap()
                    .split(',')
                    .map(|val| val.trim().parse::<u64>().unwrap())
                    .collect();

                let op = Monkey::parse_operation(monkey.next().unwrap());

                let divisible_by = monkey
                    .next()
                    .unwrap()
                    .split(' ')
                    .last()
                    .unwrap()
                    .parse::<u64>()
                    .unwrap();

                let mut decision_values = monkey.into_iter().map(|v| {
                    v.split_whitespace()
                        .last()
                        .unwrap()
                        .parse::<usize>()
                        .unwrap()
                });

                let t = decision_values.next().unwrap();
                let f = decision_values.next().unwrap();

                Monkey {
                    inspections: 0,
                    items,
                    op,
                    divisible_by,
                    t,
                    f,
                }
            })
            .collect();

        let common = monkeys
            .iter()
            .map(|monkey| monkey.divisible_by)
            .product::<u64>();

        for _ in 0..10_000 {
            for i in 0..monkeys.len() {
                let monkey = &mut monkeys[i];

                let mut items: Vec<(usize, u64)> = vec![];
                while let Some(item) = monkey.items.pop() {
                    monkey.inspections += 1;
                    let inspected_item = match monkey.op {
                        Operation::Add(x) => item + x,
                        Operation::AddSelf => item + item,
                        Operation::Multiply(x) => item * x,
                        Operation::MultiplySelf => item * item,
                    };

                    let bored_item = inspected_item % common;

                    if bored_item % monkey.divisible_by == 0 {
                        items.push((monkey.t, bored_item));
                    } else {
                        items.push((monkey.f, bored_item));
                    }
                }

                for (receiver, item) in items {
                    monkeys[receiver].items.push(item);
                }
            }
        }

        let mut inspections = monkeys
            .into_iter()
            .map(|monkey| monkey.inspections)
            .collect::<Vec<u64>>();
        inspections.sort();

        Some(
            inspections
                .iter()
                .rev()
                .take(2)
                .product::<u64>()
                .to_string(),
        )
    }
}
