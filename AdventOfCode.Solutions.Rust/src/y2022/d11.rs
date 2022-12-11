use crate::{core::{Part, Solution}, utils::string::StrUtils};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day11::solve_part_one(input),
        Part::P2 => Day11::solve_part_two(input),
    }
}

#[derive(Debug)]
enum Operation {
    Add(i32),
    AddSelf,
    Multiply(i32),
    MultiplySelf,
}

#[derive(Debug)]
struct Monkey {
    inspections: i32,
    items: Vec<i32>,
    op: Operation,
    divisible_by: i32,
    t: usize,
    f: usize,
}

impl Monkey {
    fn parse_operation(input: &str) -> Operation {
        let (_, pieces) = input.split_once("old").unwrap();
        match pieces.trim().split_once(' ').unwrap() {
            ("+", "old") => Operation::AddSelf,
            ("+", val) => Operation::Add(val.parse::<i32>().unwrap()),
            ("*", "old") => Operation::MultiplySelf,
            ("*", val) => Operation::Multiply(val.parse::<i32>().unwrap()),
            _ => panic!()
        }
    }

    // fn parse_decision(input: Vec<&str>) -> impl Fn(i32) -> i32 {
    //     let values: Vec<i32> = input.into_iter().map(|v| v.split_whitespace().last().unwrap().parse::<i32>().unwrap()).collect();

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
    fn solve_part_one(input: String) -> String {
        let mut monkeys: Vec<Monkey> = input.paragraphs().map(|paragraph| {
            let mut monkey = paragraph.lines().skip(1).map(|line| {
                let (_, val) = line.split_once(':').unwrap();
                val
            });

            let items = monkey.next().unwrap().split(',').map(|val| {
                val.trim().parse::<i32>().unwrap()
            }).collect();

            let op = Monkey::parse_operation(monkey.next().unwrap());
            
            let divisible_by = monkey.next().unwrap().split(' ').last().unwrap().parse::<i32>().unwrap();

            let mut decision_values = monkey.into_iter().map(|v| {
                v.split_whitespace().last().unwrap().parse::<usize>().unwrap()
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
        }).collect();

        for _ in 0..20 {
            for i in 0..monkeys.len() {
                let monkey = &mut monkeys[i];

                let mut items: Vec<(usize, i32)> = vec![];
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

        let mut inspections = monkeys.into_iter().map(|monkey| monkey.inspections).collect::<Vec<i32>>();
        inspections.sort();

        inspections.iter().rev().take(2).fold(1, |acc, item| acc * item).to_string()
    }

    fn solve_part_two(_input: String) -> String {
        String::new()
    }
}
